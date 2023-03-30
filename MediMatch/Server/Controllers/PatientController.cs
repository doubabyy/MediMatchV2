using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MediMatch.Server.Controllers
{
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
        [Route("api/browse-doctors")]
        public async Task<ActionResult<List<Bill>>> BrowseDoctors()
        {
            //var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var doctors = await (from u in _context.Users
                                 where u.UserType == "D"
                                 select u).ToListAsync();


            return Ok(doctors);
        }

        [HttpPost]
        [Route("api/send-request")]
        public async Task<ActionResult> SendRequest(string doc_id)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var match = new Match
                {
                    PatientId = user.Id,
                    DoctorId = doc_id,
                    RequestedAt = DateTime.Now,
                    AcceptedAt = null,
                    Accepted = false
                };
            _context.Matches.Add(match);
            return Ok();
        }
    }
}