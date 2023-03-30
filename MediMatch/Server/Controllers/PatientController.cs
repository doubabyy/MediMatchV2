using Microsoft.AspNetCore.Mvc;

namespace MediMatch.Server.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
