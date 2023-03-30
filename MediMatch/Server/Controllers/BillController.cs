using MediMatch.Server.Data;
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
            //var bills = await (from u in _context.Users
            //                   join b in _context.Bills on u.Id equals b.PatientId
            //                   where u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)
            //                    && b.Paid == true
            //                   select
            //                   b).ToListAsync

            //var bills =  await _context.Users
            //                .Join(_context.Bills, p => p.Id, b => b.PatientId, (p, b) => new { p, b })
            //                .Join(_context.Users, d => d.Id)

            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var bills = await (from p in _context.Users
                               join b in _context.Bills on p.Id equals b.PatientId
                               join d in _context.Users on b.DoctorId equals d.Id
                               where b.Paid == true
                                && p.Id == user.Id
                               orderby b.Date_received descending
                               select new BillDisplay
                               {
                                   Bill_Id = b.Bill_Id,
                                   Date_received = b.Date_received,
                                   Amount = b.Amount,
                                   DueDate = b.DueDate,
                                   PatientId = b.PatientId,
                                   PatientName = p.FirstName,
                                   DoctorId = b.DoctorId,
                                   DoctorName = d.FirstName,
                                   Paid = b.Paid 
                               }).ToListAsync();

            return Ok(bills);
        }

        [HttpGet]
        [Route("api/get-bills-upcoming")]
        public async Task<ActionResult<List<Bill>>> GetBillsUpcoming()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var bills = await (from p in _context.Users
                               join b in _context.Bills on p.Id equals b.PatientId
                               join d in _context.Users on b.DoctorId equals d.Id
                               where b.Paid == false
                                    && p.Id == user.Id
                               orderby b.DueDate descending
                               select new BillDisplay
                               {
                                   Bill_Id = b.Bill_Id,
                                   Date_received = b.Date_received,
                                   Amount = b.Amount,
                                   DueDate = b.DueDate,
                                   PatientId = b.PatientId,
                                   PatientName = p.FirstName,
                                   DoctorId = b.DoctorId,
                                   DoctorName = d.FirstName,
                                   Paid = b.Paid
                               }).ToListAsync();
            return Ok(bills);
        }

        [HttpPost]
        [Route("api/make-payment")]
        public async Task<ActionResult> MakePayment([FromBody] int bill_id)
        {
            //var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var myBill = await _context.Bills.FirstOrDefaultAsync(u => u.Bill_Id == bill_id);
            if (myBill == null)
            {
                return NotFound();
            }

            myBill.Paid = true;
            myBill.Date_received = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}