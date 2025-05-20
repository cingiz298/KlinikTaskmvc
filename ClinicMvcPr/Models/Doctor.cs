using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicMvcPr.Models
{
    public class Doctor:BaseClass
    {
        public string Name { get; set; }
        public int DepartmentId {  get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
        public Department? Department {  get; set; }

    }
}
