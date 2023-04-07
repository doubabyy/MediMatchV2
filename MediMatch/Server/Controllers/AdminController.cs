using Microsoft.AspNetCore.Mvc;
using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;

namespace MediMatch.Server.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("get-doctors")]
        public async Task<ActionResult<List<DoctorDto>>> GetDoctors()
        {
            try
            {
                var doctors = await (from d in _context.Doctors
                                     join u in _context.Users on d.ApplicationUserId equals u.Id
                                     select new DoctorDto
                                     {
                                         Id = u.Id,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Description = d.Description,
                                         Availability = d.Availability,
                                         Specialty = d.Specialty,
                                         Rates = d.Rates,
                                         AcceptsInsurance = d.AcceptsInsurance}).ToListAsync();


                return Ok(doctors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return NotFound();

        }


        [HttpGet]
        [Route("get-patients")]
        public async Task<ActionResult<List<DoctorDto>>> GetPatients()
        {

            try
            {
                var patients = await (from p in _context.Patients
                                     join u in _context.Users on p.ApplicationUserId equals u.Id
                                     select new PatientDto
                                     {
                                         Id = u.Id,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Age = -1,
                                         DOB = u.DOB,
                                         Gender = p.Gender,
                                         DepAnx = p.DepAnx,
                                         SuicThoughts = p.SuicThoughts,
                                         SuicThoughtsDesc = p.SuicThoughtsDesc,
                                         SubstanceAbuse = p.SubstanceAbuse,
                                         SubstanceAbuseDesc = p.SubstanceAbuseDesc,
                                         Therapy = p.Therapy,
                                         TherapyDesc = p.TherapyDesc,
                                         ProblemsDesc = p.ProblemsDesc,
                                         TreatmentGoals = p.TreatmentGoals}).ToListAsync();


                return Ok(patients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return NotFound();

        }

        [HttpPost]
        [Route("api/delete-user")]
        public async Task<ActionResult> RemoveMovie([FromBody] string patient_id)
        {
            var myPatient = (from u in _context.Users
                             where u.Id == patient_id
                             select u).FirstOrDefault();

            _context.Users.Remove(myPatient);
            _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("new-appointment")]
        public async Task<IActionResult> AddNewAppointment([FromBody] AppointmentDto NewAppointmentDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                /*var AptId = await (from a in _context.Appointments
                              select a.AppointmentId).LastOrDefaultAsync();
                AptId++;
                Console.WriteLine(AptId);*/
                Appointment NewAppointment = new Appointment()
                {
                    //AppointmentId = AptId,
                    DoctorId = NewAppointmentDto.DoctorId,
                    PatientId = user.Id,
                    AppointmentDateStart = NewAppointmentDto.StartTime,
                    AppointmentDateEnd = NewAppointmentDto.EndTime

                };

                _context.Appointments.Add(NewAppointment);
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok();

        }



    }




}

