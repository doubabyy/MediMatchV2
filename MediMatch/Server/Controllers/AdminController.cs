using Microsoft.AspNetCore.Mvc;
using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Drawing.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace MediMatch.Server.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("get-doctors")]
        public async Task<ActionResult<List<DoctorDto>>> GetDoctors()
        {
            try
            {
                var doctors = await (from d in _context.Doctors
                                     join u in _context.Users on d.ApplicationUserId equals u.Id
                                     select new DoctorDto
                                     {
                                         Id = u.Id,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Description = d.Description,
                                         Availability = d.Availability,
                                         Specialty = d.Specialty,
                                         Rates = d.Rates,
                                         AcceptsInsurance = d.AcceptsInsurance}).ToListAsync();


                return Ok(doctors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return NotFound();

        }


        [HttpGet]
        [Route("get-patients")]
        public async Task<ActionResult<List<PatientDto>>> GetPatients()
        {

            try
            {
                var patients = await (from p in _context.Patients
                                     join u in _context.Users on p.ApplicationUserId equals u.Id
                                     select new PatientDto
                                     {
                                         Id = u.Id,
                                         FirstName = u.FirstName,
                                         LastName = u.LastName,
                                         Age = -1,
                                         DOB = u.DOB,
                                         Gender = p.Gender,
                                         DepAnx = p.DepAnx,
                                         SuicThoughts = p.SuicThoughts,
                                         SuicThoughtsDesc = p.SuicThoughtsDesc,
                                         SubstanceAbuse = p.SubstanceAbuse,
                                         SubstanceAbuseDesc = p.SubstanceAbuseDesc,
                                         Therapy = p.Therapy,
                                         TherapyDesc = p.TherapyDesc,
                                         ProblemsDesc = p.ProblemsDesc,
                                         TreatmentGoals = p.TreatmentGoals}).ToListAsync();


                return Ok(patients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return NotFound();
        }

        [HttpPost]
        [Route("delete-user")]
        public async Task<ActionResult> RemoveUser([FromBody] string patient_id)
        {
            var myPatient = await (from u in _context.Users
                             where u.Id == patient_id
                             select u).FirstOrDefaultAsync();

            _context.Users.Remove(myPatient);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("get-bills-history")]
        public async Task<ActionResult<List<BillDisplay>>> GetBillsHistory()
        {
            var bills = await (from p in _context.Users
                               join b in _context.Bills on p.Id equals b.PatientId
                               join d in _context.Users on b.DoctorId equals d.Id
                               where b.Paid == true
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
        [Route("get-bills-upcoming")]
        public async Task<ActionResult<List<BillDisplay>>> GetBillsUpcoming()
        {
            var bills = await (from p in _context.Users
                               join b in _context.Bills on p.Id equals b.PatientId
                               join d in _context.Users on b.DoctorId equals d.Id
                               where b.Paid == false
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

        [HttpPost]
        [Route("delete-bill")]
        public async Task<ActionResult> DeleteBill([FromBody] int bill_id)
        {
            var myBill = await (from u in _context.Bills
                             where u.Bill_Id == bill_id
                             select u).FirstOrDefaultAsync();

            _context.Bills.Remove(myBill);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        [Route("get-accepted-requests")]
        public async Task<ActionResult<List<MatchDto>>> GetAcceptedRequests()
        {
            var requests = await (from m in _context.Matches
                                  join d in _context.Users on m.DoctorId equals d.Id
                                  join p in _context.Users on m.PatientId equals p.Id
                                  where m.Accepted == true
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

        [HttpGet]
        [Route("get-rejected-requests")]
        public async Task<ActionResult<List<MatchDto>>> GetRejectedRequests()
        {
            var requests = await (from m in _context.Matches
                                  join d in _context.Users on m.DoctorId equals d.Id
                                  join p in _context.Users on m.PatientId equals p.Id
                                  where m.Accepted == false
                                    && m.RejectedAt != null
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

        [HttpGet]
        [Route("gpr")]
        public async Task<ActionResult<List<MatchDto>>> GetPendingRequests()
        {
            var requests = await (from m in _context.Matches
                                  join d in _context.Users on m.DoctorId equals d.Id
                                  join p in _context.Users on m.PatientId equals p.Id
                                  where m.Accepted == false
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

        [HttpPost]
        [Route("delete-match")]
        public async Task<ActionResult> DeleteMatch([FromBody] int match_id)
        {
            var myMatch = await (from u in _context.Matches
                                where u.Id == match_id
                                select u).FirstOrDefaultAsync();

            _context.Matches.Remove(myMatch);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }




}

