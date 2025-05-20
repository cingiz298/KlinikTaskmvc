using ClinicMvcPr.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvcPr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            string connectionstr = @"Server=WINDOWS-K6JIO6V\SQLEXPRESS;Database=KlinikDb;TrustServerCertificate=True;Trusted_Connection=True";
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionstr));


            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=index}/{id?}"
                );
            

            app.Run();
        }
    }
}
