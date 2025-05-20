using ClinicMvcPr.Contexts;
using ClinicMvcPr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvcPr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public DoctorController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<Doctor> doctors = _context.Doctors.Include(d => d.Department).ToList();
            return View(doctors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if(!doctor.ImageUpload.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageUpload", "duzugun formatda yukle");
                return View(doctor);
            }
            string filename=Guid.NewGuid() + doctor.ImageUpload.FileName;
            string path =_environment.WebRootPath + filename;
            using(FileStream fileStream =new FileStream(path, FileMode.Create))
            {
                doctor.ImageUpload.CopyTo(fileStream);
            }
            if (!ModelState.IsValid) { return View(nameof(Create)); }
            doctor.ImgUrl=filename;
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Doctor? doctor = _context.Doctors.Find(id);
            if (doctor == null) { return NotFound("Tapilmadi"); }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Doctor? existingDoctors = _context.Doctors.Find(id);
            if (existingDoctors == null) { return NotFound("Tapilmadi"); }
            return View(nameof(Create), existingDoctors);
        }
        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            if (!ModelState.IsValid) { return View(nameof(Create), doctor); }
            Doctor? existingDoctor = _context.Doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existingDoctor == null)
            {
                return NotFound("tapilmadi");
            }
            existingDoctor.Name = doctor.Name;
            existingDoctor.ImgUrl = doctor.ImgUrl;
            existingDoctor.DepartmentId = doctor.DepartmentId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
