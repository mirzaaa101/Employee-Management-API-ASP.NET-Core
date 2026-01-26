using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Employee_Management_API_ASP.NET_Core.Models
{
    public class EmployeeBasicInfo
    {
        [Key]
        public int EmpId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; private set; }
        public void SetPlaceholderEmail()
        {
            this.Email = "temp@temp.com";
        }
        public void GenerateEmail()
        {
            string cleanFirstName = FirstName.Replace(" ", "").ToLower();
            this.Email = $"{cleanFirstName}{EmpId}@gmail.com";
        }

        // Foreign Keys
        public int PayGrade { get; set; }
        [ForeignKey("PayGrade")] 
        public virtual PayGrade? PayGradeNavigation { get; set; }
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]
        public virtual Departments? DepartmentNavigation { get; set; }

    }
}