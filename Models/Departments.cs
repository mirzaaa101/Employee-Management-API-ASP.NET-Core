using System.ComponentModel.DataAnnotations;
namespace Employee_Management_API_ASP.NET_Core.Models
{
    public class Departments
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; } = string.Empty;
    }
}