using ClinicMvcPr.Contexts;
using ClinicMvcPr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvcPr.Controllers
{
    public class HomeController : Controller
    {
       private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Doctor> doctors=_context.Doctors.Include(d=>d.Department).ToList();
            return View(doctors);
        }
    }
}
