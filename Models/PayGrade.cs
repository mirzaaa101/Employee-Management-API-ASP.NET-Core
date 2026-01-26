using System.ComponentModel.DataAnnotations;

namespace Employee_Management_API_ASP.NET_Core.Models
{
    public class PayGrade
    {
        [Key]
        public int GradeNo{ get; set; }
        public double Salary{ get; set; }
    }
}