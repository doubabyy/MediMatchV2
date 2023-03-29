﻿using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

namespace MediMatch.Server.Controllers
{
    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/get-bills-history")]
        public async Task<ActionResult<List<Bill>>> GetBillsHistory()
        {
            var bills = await (from u in _context.Users
                               join b in _context.Bills on u.Id equals b.UserId
                               where u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                && b.Paid == true
                               select
                               b).ToListAsync();
            return Ok(bills);
        }

        [HttpGet]
        [Route("api/get-bills-upcoming")]
        public async Task<ActionResult<List<Bill>>> GetBillsUpcoming()
        {
            var bills = await (from u in _context.Users
                               join b in _context.Bills on u.Id equals b.UserId
                               where u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                && b.Paid == false
                               select
                               b).ToListAsync();
            return Ok(bills);
        }
    }
}
