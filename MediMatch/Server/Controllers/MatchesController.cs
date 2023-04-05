using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MediMatch.Server.Controllers
{
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MatchesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Request
        [HttpGet]
        [Route("api/get-requests")]
        public async Task<ActionResult<List<Match>>> GetRequests()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var requests = await (from m in _context.Matches
                                  join d in _context.Users on m.DoctorId equals d.Id
                                  join p in _context.Users on m.PatientId equals p.Id
                                  where m.DoctorId == user.Id
                                    && m.Accepted == false
                                    && m.RejectedAt == null
                                orderby m.RequestedAt descending
                                select new MatchDto
                                {
                                    Id = m.Id,
                                    PatientId = m.PatientId,
                                    DoctorId = m.DoctorId,
                                    RequestedAt = m.RequestedAt,
                                    AcceptedAt = m.AcceptedAt,
                                    RejectedAt = m.RejectedAt,
                                    Accepted = m.Accepted,
                                    PatientName = p.FirstName + " " + p.LastName,
                                    DoctorName = d.FirstName + " " + d.LastName
                                }).ToListAsync();
            return Ok(requests);
        }

        // POST: api/Request
        [HttpPost]
        [Route("api/accept-request")]
        public async Task<ActionResult> AcceptRequest([FromBody] int request_id)
        {
            
            try
            {
                Match myRequest = await _context.Matches.FirstOrDefaultAsync(u => u.Id == request_id);
                myRequest.Accepted = true;
                myRequest.AcceptedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            { 
                Console.Write(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        // add a reject request 
        [HttpPost]
        [Route("api/reject-request")]
        public async Task<ActionResult> RejectRequest([FromBody] int request_id)
        {
            var myRequest = await _context.Matches.FirstOrDefaultAsync(u => u.Id == request_id);
            myRequest.Accepted = false; 
            myRequest.RejectedAt = DateTime.Now; 
            await _context.SaveChangesAsync();
            return Ok();
        }


        // GET: api/Request/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Request>> GetRequest(int id)
        //{
        //    var request = await _context.Requests.FindAsync(id);

        //    if (request == null)
        //    {
        //        return NotFound();
        //    }

        //    return request;
        //}
    }
}