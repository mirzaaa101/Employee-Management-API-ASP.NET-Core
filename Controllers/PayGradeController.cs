using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Employee_Management_API_ASP.NET_Core.Data;
using Employee_Management_API_ASP.NET_Core.Models;

namespace Employee_Management_API_ASP.NET_Core.Controllers
{
    public class PayGradeController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PayGradeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetPayGrade")]
        public async Task<ActionResult<IEnumerable<PayGrade>>> GetPayGrades()
        {
            return await _context.PayGrade.ToListAsync(); 
        }
        [HttpPost("AddPayGrade")]
        public async Task<ActionResult<PayGrade>> AddPayGrade(PayGrade payGrade)
        {
            _context.PayGrade.Add(payGrade);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPayGrades", new { GradeNo = payGrade.GradeNo, salary = payGrade.Salary }, payGrade);
        }
    }
}