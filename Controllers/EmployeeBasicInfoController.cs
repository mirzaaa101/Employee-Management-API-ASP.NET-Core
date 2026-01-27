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
                    FullName = e.FirstName + " " + e.LastName,
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
        [HttpPatch("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int EmpId, [FromBody] EmployeeUpdateDto updateDto)
        {
            var employee = await _context.EmployeeBasicInfo.FindAsync(EmpId);
            if (employee == null) return NotFound($"Employee with ID {EmpId} not found.");
            if (updateDto.PayGrade.HasValue && updateDto.PayGrade.Value != 0)
            {
                var payGradeExists = await _context.PayGrade.AnyAsync(pg => pg.GradeNo == updateDto.PayGrade.Value);
                if (!payGradeExists)
                    return BadRequest($"PayGrade {updateDto.PayGrade.Value} does not exist.");

                employee.PayGrade = updateDto.PayGrade.Value;
            }
            if (updateDto.DeptId.HasValue && updateDto.DeptId.Value != 0)
            {
                var deptExists = await _context.Departments.AnyAsync(d => d.DeptId == updateDto.DeptId.Value);
                if (!deptExists)
                    return BadRequest($"Department ID {updateDto.DeptId.Value} does not exist.");

                employee.DeptId = updateDto.DeptId.Value;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Employee updated successfully",
                employeeId = EmpId,
                employee.PayGrade,
                employee.DeptId
            });
        }
    }
}