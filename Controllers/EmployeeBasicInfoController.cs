using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_Management_API_ASP.NET_Core.Data;
using Employee_Management_API_ASP.NET_Core.Models;

namespace Employee_Management_API_ASP.NET_Core.Controllers
{
    public class EmployeeBasicInfoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeBasicInfoController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetEmployeeBasicInfo")]
        public async Task<IActionResult> GetEmployeeBasicInfo()
        {
            var employees = await _context.EmployeeBasicInfo
                .Include(e => e.PayGradeNavigation)
                .Include(e => e.DepartmentNavigation)
                .Select(e => new
                {
                    e.EmpId,
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    Salary = e.PayGradeNavigation != null ? e.PayGradeNavigation.Salary : 0,
                    DepartmentName = e.DepartmentNavigation != null ? e.DepartmentNavigation.DeptName : "N/A"
                })
                .ToListAsync();

            return Ok(employees);
        }
        [HttpPost("AddEmployees")]
        public async Task<ActionResult<EmployeeBasicInfo>> AddEmployeeBasicInfo(EmployeeBasicInfo employee)
        {
            employee.SetPlaceholderEmail();
            _context.EmployeeBasicInfo.Add(employee);
            await _context.SaveChangesAsync();
            employee.GenerateEmail();
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmployeeBasicInfo",
            new
            {
                EmpId = employee.EmpId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PayGrade = employee.PayGrade,
                DeptId = employee.DeptId
            }
            );
        }
    }
}