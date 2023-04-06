﻿using Microsoft.AspNetCore.Mvc;
using MediMatch.Server.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MediMatch.Server.Data;
using MediMatch.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediMatch.Server.Controllers
{
 
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public MessageController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("GetUsernameById/{user_id}")]
        public async Task<ActionResult<string>> GetUsernameById([FromRoute] string user_id)
        {
            var myUsername = await (from u in _context.Users
                                    where u.Id == user_id
                                    select u.FirstName).FirstOrDefaultAsync();
            return Ok(myUsername);
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<ApplicationUserDto>>> GetUsers()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user.UserType == "d" || user.UserType == "D")
            {
                var myUsers = await (from m in _context.Matches
                                     join p in _context.Users on m.PatientId equals p.Id
                                     where m.DoctorId == user.Id
                                     orderby p.FirstName
                                     select new ApplicationUserDto
                                     {
                                         Id = p.Id,
                                         FirstName = p.FirstName,
                                         MiddleInitial = p.MiddleInitial,
                                         LastName = p.LastName,
                                         Suffix = p.Suffix,
                                         DOB = p.DOB,
                                         Address = p.Address,
                                         UserType = p.UserType
                                     }).ToListAsync();
                return Ok(myUsers);
            }
            else
            {
                var myUsers = await (from m in _context.Matches
                                     join p in _context.Users on m.DoctorId equals p.Id
                                     where m.PatientId == user.Id
                                     orderby p.FirstName
                                     select new ApplicationUserDto
                                     {
                                         Id = p.Id,
                                         FirstName = p.FirstName,
                                         MiddleInitial = p.MiddleInitial,
                                         LastName = p.LastName,
                                         Suffix = p.Suffix,
                                         DOB = p.DOB,
                                         Address = p.Address,
                                         UserType = p.UserType
                                     }).ToListAsync();
                return Ok(myUsers);
            }

            return BadRequest();

        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<String>> GetUserId()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(user);
        }



        // get messages between users method 
        [HttpGet("GetMessagesBetweenUsers/{userId1}/{userId2}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBetweenUsers(string userId1, string userId2)
        {
            try
            {
                //  var messages = await _context.Messages
                //.Include(m => m.MessageFromID)
                //.Include(m => m.MessageToID)
                //.Where(m => (m.MessageFromID == userId1 && m.MessageToID == userId2) || (m.MessageFromID == userId2 && m.MessageToID == userId1))
                //.ToListAsync()
                var messages = await (from m in _context.Messages
                                      where ((m.MessageFromID == userId1) && (m.MessageToID == userId2))
                                      || (m.MessageFromID == userId2 && m.MessageToID == userId1)
                                      select m).ToListAsync();
                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new List<Message>();

        }
        [HttpPost]
        [Route("api/SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest("Message is null");
            }

            if (_context == null)
            {
                return BadRequest("ApplicationDbContext is null");
            }

            if (string.IsNullOrEmpty(message.MessageFromID))
            {
                return BadRequest("MessageFromID is empty or null");
            }

            if (string.IsNullOrEmpty(message.MessageToID))
            {
                return BadRequest("MessageToID is empty or null");
            }

            if (string.IsNullOrEmpty(message.MessageTxt))
            {
                return BadRequest("MessageTxt is empty or null");
            }

            message.MessageDate = DateTime.Now;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("GetCurrentUserId")]
        public async Task<ActionResult<string>> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                Console.WriteLine("GetCurrentUserId: User not found."); // Debugging
                return NotFound();
            }

            Console.WriteLine($"GetCurrentUserId: {user.Id}"); // Debugging
            return user.Id;
        }


    }



}