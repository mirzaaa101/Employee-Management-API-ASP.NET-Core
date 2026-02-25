using Microsoft.AspNetCore.Mvc;
using Employee_Management_API_ASP.NET_Core.Services;

namespace Employee_Management_API_ASP.NET_Core.Controllers
{
    public class EmployeeBasicInfoController : ControllerBase
    {

        private readonly IEmployeeService _employeeService;
        public EmployeeBasicInfoController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost("GetEmployeeBasicInfo")]
        public async Task<IActionResult> GetEmployeeBasicInfo([FromBody] int EmpId)
        {
            try
            {
                var result = await _employeeService.GetEmployeeBasicInfo(EmpId);
                if (result == null)
                {
                    return NotFound(new { message = $"Employee with ID {EmpId} was not found." });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    message = "An internal error occurred while processing your request.",
                });
            }
        }
    }
}