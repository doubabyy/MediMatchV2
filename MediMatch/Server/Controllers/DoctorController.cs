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
    public class DoctorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DoctorController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        
        
        [HttpGet("api/doctor-profile")]
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
     


        [HttpPut("api/doctor-profile")]
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
        [HttpGet]
        [Route("GetRequests/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetRequests(string doctorId)
        {
            return await _context.Matches.Where(r => r.DoctorId == doctorId).ToListAsync();
        }

    }
}
