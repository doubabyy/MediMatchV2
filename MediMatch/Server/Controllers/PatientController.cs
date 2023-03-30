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
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PatientController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("CreateRequest")]
        public async Task<ActionResult<Request>> CreateRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateRequest), new { id = request.RequestID }, request);
        }
        [HttpGet("api/request/{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        [HttpPost("api/patient/send-request")]
        public async Task<ActionResult> SendRequest([FromBody] Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRequest), new { id = request.RequestID }, request);
        }
    }

}
