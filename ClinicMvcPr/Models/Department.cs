namespace ClinicMvcPr.Models
{
    public class Department:BaseClass
    {
        public string Title {  get; set; }
        public ICollection<Doctor>? Doctor { get; set; }
    }
}
