using ClinicMvcPr.Contexts;
using ClinicMvcPr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvcPr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Department> departments =_context.Departments.Include(dep=>dep.Doctor).ToList();
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(!ModelState.IsValid) { return View(nameof(Create)); }
            _context.Departments.Add(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Department? department=_context.Departments.Find(id);
            if (department == null) {  return NotFound("Tapilmadi"); } 
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Department? existingDepartment = _context.Departments.Find(id);
            if (existingDepartment == null) { return NotFound("Tapilmadi"); }
            return View(nameof(Create), existingDepartment);
        }
        [HttpPost]
        public IActionResult Update(Department department)
        {
            if (!ModelState.IsValid) { return View(nameof(Create), department); }
            Department? existingDepartment = _context.Departments.FirstOrDefault(dep => dep.Id == department.Id);
            if(existingDepartment == null)
            {
                return NotFound("tapilmadi");
            }
            existingDepartment.Title = department.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

    }
}
