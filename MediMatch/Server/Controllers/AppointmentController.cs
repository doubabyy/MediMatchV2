using Microsoft.AspNetCore.Mvc;
using MediMatch.Server.Data;
using MediMatch.Server.Models;
using MediMatch.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;

namespace MediMatch.Server.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AppointmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var doctors = await (from u in _context.Users
                                     join m in _context.Matches on u.Id equals m.DoctorId
                                     where m.PatientId == user.Id
                                     select new DoctorDto
                                    {
                                        Id = u.Id,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName
                                    }).ToListAsync();


                return Ok(doctors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return NotFound();

        }
        

        [HttpGet]
        [Route("get-doctor-availability/{id}")]
        public async Task<ActionResult<List<AppointmentDto>>> GetDoctorAvailability(string id)
        {
            
            var appointmentList = await (from d in _context.Appointments
                              where d.DoctorId == id
                              select new AppointmentDto
                              { 
                                  StartTime = d.AppointmentDateStart,
                                  EndTime = d.AppointmentDateEnd
                              }).ToListAsync();

            return Ok(appointmentList);

        }
        
        
        [HttpPost]
        [Route("new-appointment")]
        public async Task<IActionResult> AddNewAppointment([FromBody] AppointmentDto NewAppointmentDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                /*var AptId = await (from a in _context.Appointments
                              select a.AppointmentId).LastOrDefaultAsync();
                AptId++;
                Console.WriteLine(AptId);*/
                Appointment NewAppointment = new Appointment()
                {
                    //AppointmentId = AptId,
                    DoctorId = NewAppointmentDto.DoctorId,
                    PatientId = user.Id,
                    AppointmentDateStart = NewAppointmentDto.StartTime,
                    AppointmentDateEnd = NewAppointmentDto.EndTime

                };
                    
                _context.Appointments.Add(NewAppointment);
                await _context.SaveChangesAsync();
                

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok();

        }

        [HttpGet]
        [Route("get-appointments")]
        public async Task<ActionResult<List<AppointmentDto>>> GetDoctorAppointments()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var appointmentList = await (from d in _context.Appointments
                                         where d.DoctorId == user.Id
                                         select new AppointmentDto
                                         {
                                             StartTime = d.AppointmentDateStart,
                                             EndTime = d.AppointmentDateEnd
                                         }).ToListAsync();

            return Ok(appointmentList);

        }


    }

  


}

