using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MediMatch.Server.Models;
using MediMatch.Server.Data;
using MediMatch.Shared;
using System.Linq;

namespace MediMatch.Server.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DoctorController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpGet]
        [Route("get-doctor/{doctor_id}")]
        public async Task<ActionResult<DoctorDto?>> GetDoctorDetail([FromRoute] string doctor_id)
        {
            try
            {
                var doctor = await (from p in _context.Doctors
                                     join u in _context.Users on p.ApplicationUserId equals u.Id
                                     where u.Id == doctor_id
                                     select new DoctorDto
                                     {
                                         Id = doctor_id,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Description = p.Description,
                                         Availability = p.Availability,
                                         Specialty = p.Specialty,
                                         Rates = p.Rates,
                                         AcceptsInsurance = p.AcceptsInsurance
                                     }).FirstOrDefaultAsync();
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return BadRequest(new DoctorDto());

        }

        [HttpGet]
        [Route("get-my-patients")]
        public async Task<ActionResult<List<PatientDto>>> GetMyPatients()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var patients = await (from p in _context.Patients
                                    join u in _context.Users on p.ApplicationUserId equals u.Id
                                    join m in _context.Matches on p.ApplicationUserId equals m.PatientId
                                  where m.DoctorId == user.Id 
                                    &&m.Accepted == true
                                    select new PatientDto
                                    {
                                        Id = p.ApplicationUserId,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Age = -1,
                                        DOB = u.DOB,
                                        Gender = p.Gender,
                                        DepAnx = p.DepAnx,
                                        DepAnxDesc = p.DepAnxDesc,
                                        SuicThoughts = p.SuicThoughts,
                                        SuicThoughtsDesc = p.SuicThoughtsDesc,
                                        SubstanceAbuse = p.SubstanceAbuse,
                                        SubstanceAbuseDesc = p.SubstanceAbuseDesc,
                                        SupportSystem = p.SupportSystem,
                                        Therapy = p.Therapy,
                                        TherapyDesc = p.TherapyDesc,
                                        ProblemsDesc = p.ProblemsDesc,
                                        TreatmentGoals = p.TreatmentGoals
                                    }).ToListAsync();
            for(int i = 0; i < patients.Count; i++)
            {
                var today = DateTime.Today;
                int age = today.Year - patients[i].DOB.Year;
                // Go back to the year in which the person was born in case of a leap year
                if (patients[i].DOB.Date > today.AddYears(-age)) age--;
                patients[i].Age = age;
            }
            return patients;
        }

        [HttpGet]
        [Route("doctor-profile")]
        public async Task<ActionResult<DoctorDto?>> GetDoctor()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Doctor? doctor = await _context.Doctors.FindAsync(user.Id);

            var dto = new DoctorDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            if (doctor != null)
            {
                dto.Description = doctor.Description;
                dto.Availability = doctor.Availability;
                dto.Specialty = doctor.Specialty;
                dto.Rates = doctor.Rates;
                dto.AcceptsInsurance = doctor.AcceptsInsurance;
            }


            return dto;

        }

        [HttpGet]
        [Route("get-bills-history")]
        public async Task<ActionResult<List<BillDisplay>>> GetBillsHistory()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var bills = await (from p in _context.Users
                               join b in _context.Bills on p.Id equals b.PatientId
                               join d in _context.Users on b.DoctorId equals d.Id
                               where b.Paid == true
                                && d.Id == user.Id
                               orderby b.Date_received descending
                               select new BillDisplay
                               {
                                   Bill_Id = b.Bill_Id,
                                   Date_received = b.Date_received,
                                   Amount = b.Amount,
                                   DueDate = b.DueDate,
                                   PatientId = b.PatientId,
                                   PatientName = p.FirstName,
                                   DoctorId = b.DoctorId,
                                   DoctorName = d.FirstName,
                                   Paid = b.Paid
                               }).ToListAsync();
            return Ok(bills);
        }

        [HttpGet]
        [Route("get-bills-upcoming")]
        public async Task<ActionResult<List<BillDisplay>>> GetBillsUpcoming()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var bills = await (from p in _context.Users
                               join b in _context.Bills on p.Id equals b.PatientId
                               join d in _context.Users on b.DoctorId equals d.Id
                               where b.Paid == false
                                    && d.Id == user.Id
                               orderby b.DueDate descending
                               select new BillDisplay
                               {
                                   Bill_Id = b.Bill_Id,
                                   Date_received = b.Date_received,
                                   Amount = b.Amount,
                                   DueDate = b.DueDate,
                                   PatientId = b.PatientId,
                                   PatientName = p.FirstName,
                                   DoctorId = b.DoctorId,
                                   DoctorName = d.FirstName,
                                   Paid = b.Paid
                               }).ToListAsync();
            return Ok(bills);
        }

           
        [HttpPut]
        [Route("doctor-profile")]
        public async Task<IActionResult> UpdateDoctorDetails( [FromBody] DoctorDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            Doctor? doctor = await _context.Doctors.FindAsync(user.Id);

            if (doctor != null)
            {
                doctor.Specialty = dto.Specialty;
                doctor.Description = dto.Description;
                doctor.Availability = dto.Availability;
                doctor.Rates = dto.Rates;
                doctor.AcceptsInsurance = dto.AcceptsInsurance;

            }
            else
            {
                doctor = new Doctor()
                {
                    ApplicationUserId = user.Id,
                    Specialty = dto.Specialty,
                    Description = dto.Description,
                    Availability = dto.Availability,
                    Rates = dto.Rates,
                    AcceptsInsurance = dto.AcceptsInsurance
                };
                _context.Doctors.Add(doctor);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();


        }
                   

        
    }
}
