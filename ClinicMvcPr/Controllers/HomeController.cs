using Microsoft.AspNetCore.Mvc;

namespace ClinicMvcPr.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
