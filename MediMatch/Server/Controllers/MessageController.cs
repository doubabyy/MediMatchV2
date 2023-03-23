using Microsoft.AspNetCore.Mvc;
using MediMatch.Server.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MediMatch.Server.Data;
using MediMatch.Shared;

namespace MediMatch.Server.Controllers
{
      
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/GetMessages")]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            var messages = await (from u in _context.Users
                                  join m in _context.Messages on u.Id equals m.MessageToID
                                  where u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                  select
                                  m

                                  ).ToListAsync();
            return Ok(messages);
        }
    }
}
