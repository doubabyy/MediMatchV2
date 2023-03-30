using Microsoft.AspNetCore.Mvc;

namespace MediMatch.Server.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
