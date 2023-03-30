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
        public async Task<ActionResult<DoctorDto>> GetDoctor()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Console.WriteLine(user.FirstName);
            Doctor doctor = user.Doctor;
            var dto = new DoctorDto();

            if (doctor != null) {
                dto.Id = user.Id;
                dto.FirstName = user.FirstName;
                dto.LastName = user.LastName;
                dto.Description = doctor.Description;
                dto.Availability = doctor.Availability;
                dto.Specialty = doctor.Specialty;
                dto.Rates = doctor.Rates;
                dto.AcceptsInsurance = doctor.AcceptsInsurance;
            }

            return dto;

        }

        [HttpPut("api/doctor-profile")]
        public async Task<IActionResult> UpdateDoctorDetails([FromBody] DoctorDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user.Doctor == null)
            {
                var doctor = new Doctor
                {
                    ApplicationUserId = user.Id,
                    Description = dto.Description,
                    Availability = dto.Availability,
                    Specialty = dto.Specialty,
                    Rates = dto.Rates,
                    AcceptsInsurance = dto.AcceptsInsurance
                };
                user.Doctor = doctor;
                _context.Doctor = doctor;

            }
               
            _context.Entry(user.Doctor).State = EntityState.Modified;

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
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests(string doctorId)
        {
            return await _context.Requests.Where(r => r.DoctorId == doctorId).ToListAsync();
        }

    }
}
