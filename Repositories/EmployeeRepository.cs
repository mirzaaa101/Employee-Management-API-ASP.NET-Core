using Employee_Management_API_ASP.NET_Core.Data;
using Employee_Management_API_ASP.NET_Core.Models.DTOs;
using Employee_Management_API_ASP.NET_Core.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Employee_Management_API_ASP.NET_Core.Repositories
{
    public class EmployeeRepository: IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeRepository(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<object> GetEmployeeBasicInfo(int EmpId)
        {
            var path = Path.Combine(
            _env.ContentRootPath,
            "Repositories",
            "SQL",
            "query.sql"
            );
            var sql = await File.ReadAllTextAsync(path);
            var result = await _context.Set<EmployeeBasicInfoDto>()
                .FromSqlRaw(sql, new MySqlParameter("@EmpId", EmpId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return result;
        }
    }
}