using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_Management_API_ASP.NET_Core.Data;
using Employee_Management_API_ASP.NET_Core.Models;

namespace Employee_Management_API_ASP.NET_Core.Controllers
{
    public class DepartmentController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetDepartments")]
        public async Task<ActionResult<IEnumerable<Departments>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
        [HttpPost("AddDepartment")]
        public async Task<ActionResult<Departments>> AddDepartment(Departments department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDepartments", new { DeptId = department.DeptId, DeptNam = department.DeptName }, department);
        }
    }
}