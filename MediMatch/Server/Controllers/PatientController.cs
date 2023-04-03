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

        //deal with case when patient is not registered
        [HttpGet]
        [Route("get-patient")]
        public async Task<ActionResult<PatientDto>> GetPatient([FromBody] string patient_id)
        {
            var patient = await (from p in _context.Patients
                                 join u in _context.Users on p.ApplicationUserId equals u.Id
                                 where u.Id == patient_id
                                 select new PatientDto
                                 {
                                     Id = patient_id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     Age = p.Age,
                                     Gender = p.Gender,
                                     Description = p.Description
                                  }).ToListAsync();
            return Ok(patient);
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
    }
}