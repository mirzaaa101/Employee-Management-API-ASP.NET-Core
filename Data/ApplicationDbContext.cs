using Microsoft.EntityFrameworkCore;
using Employee_Management_API_ASP.NET_Core.Models;
using Employee_Management_API_ASP.NET_Core.Models.DTOs;

namespace Employee_Management_API_ASP.NET_Core.Data
{
    public class ApplicationDbContext:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Departments>()
            .HasKey(x => x.DeptId);

        modelBuilder.Entity<PayGrade>()
            .HasKey(x => x.GradeNo);

        modelBuilder.Entity<EmployeeBasicInfo>()
            .HasKey(x => x.EmpId);

        modelBuilder.Entity<EmployeeBasicInfoDto>()
            .HasNoKey();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<Departments> Departments { get; set; }
        public DbSet<PayGrade> PayGrade { get; set; }
        public DbSet<EmployeeBasicInfo> EmployeeBasicInfo { get; set; }
        public DbSet<EmployeeBasicInfoDto> EmployeeBasicInfoDtos { get; set; }
    }
}