using Microsoft.EntityFrameworkCore;
using Employee_Management_API_ASP.NET_Core.Models;

namespace Employee_Management_API_ASP.NET_Core.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<Departments> Departments { get; set; }
        public DbSet<PayGrade> PayGrade { get; set; }
        public DbSet<EmployeeBasicInfo> EmployeeBasicInfo { get; set; }
    }
}