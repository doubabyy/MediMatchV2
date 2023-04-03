using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MediMatch.Server.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PatientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("browse-doctors")]
        public async Task<ActionResult<List<DoctorDto>>> BrowseDoctors()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var doctors = await (from u in _context.Doctors
                                 join appuser in _context.Users on u.ApplicationUserId equals appuser.Id
                                 where (from d in _context.Doctors 
                                        join m in _context.Matches on d.ApplicationUserId equals m.DoctorId
                                        where m.PatientId == user.Id &&
                                            d.ApplicationUserId == u.ApplicationUserId
                                        select d).Count() == 0
                                 select new DoctorDto
                                 {
                                     Id = u.ApplicationUserId,
                                     FirstName = appuser.FirstName,
                                     LastName = appuser.LastName,
                                     Description = u.Description,
                                     Availability = u.Availability,
                                     Specialty = u.Specialty,
                                     Rates = u.Rates,
                                     AcceptsInsurance = u.AcceptsInsurance
                                 }).ToListAsync();

            return Ok(doctors);
        }

        [HttpPost]
        [Route("send-request")]
        public async Task<ActionResult> SendRequest([FromBody] string doc_id)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                return NotFound();
            }

            var match = new Match
                {
                    PatientId = user.Id,
                    DoctorId = doc_id,
                    RequestedAt = DateTime.Now,
                    AcceptedAt = null,
                    RejectedAt = null,
                    Accepted = false
                };
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("patient-profile")]
        public async Task<ActionResult<PatientDto?>> GetPatient()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            Patient? patient = await _context.Patients.FindAsync(user.Id);

            var dto = new PatientDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DOB = user.DOB
            };

            if (patient != null)
            {
                dto.Gender = patient.Gender;
                dto.DepAnx = patient.DepAnx;
                dto.DepAnxDesc = patient.DepAnxDesc;
                dto.SuicThoughts = patient.SuicThoughts;
                dto.SuicThoughtsDesc = patient.SuicThoughtsDesc;
                dto.SubstanceAbuse = patient.SubstanceAbuse;
                dto.SubstanceAbuseDesc = patient.SubstanceAbuseDesc;
                dto.SupportSystem = patient.SupportSystem;
                dto.Therapy = patient.Therapy;
                dto.TherapyDesc = patient.TherapyDesc;
                dto.ProblemsDesc = patient.ProblemsDesc;
                dto.TreatmentGoals = patient.TreatmentGoals;

            }

            return dto;

        }

        [HttpPut]
        [Route("patient-profile")]
        public async Task<IActionResult> UpdatePatientDetails([FromBody] PatientDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            Patient? patient = await _context.Patients.FindAsync(user.Id);

            if (patient != null)
            {
                patient.Gender = dto.Gender;
                patient.DepAnx = dto.DepAnx;
                patient.DepAnxDesc = dto.DepAnxDesc;
                patient.SuicThoughts = dto.SuicThoughts;
                patient.SuicThoughtsDesc = dto.SuicThoughtsDesc;
                patient.SubstanceAbuse = dto.SubstanceAbuse;
                patient.SubstanceAbuseDesc = dto.SubstanceAbuseDesc;
                patient.SupportSystem = dto.SupportSystem;
                patient.Therapy = dto.Therapy;
                patient.TherapyDesc = dto.TherapyDesc;
                patient.ProblemsDesc = dto.ProblemsDesc;
                patient.TreatmentGoals = dto.TreatmentGoals;

            }
            else
            {
                patient = new Patient()
                {
                    ApplicationUserId = user.Id,
                    DepAnx = dto.DepAnx,
                    DepAnxDesc = dto.DepAnxDesc,
                    SuicThoughts = dto.SuicThoughts,
                    SuicThoughtsDesc = dto.SuicThoughtsDesc,
                    SubstanceAbuse = dto.SubstanceAbuse,
                    SubstanceAbuseDesc = dto.SubstanceAbuseDesc,
                    SupportSystem = dto.SupportSystem,
                    Therapy = dto.Therapy,
                    TherapyDesc = dto.TherapyDesc,
                    ProblemsDesc = dto.ProblemsDesc,
                    TreatmentGoals = dto.TreatmentGoals
                };
                _context.Patients.Add(patient);
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