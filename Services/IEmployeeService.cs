namespace Employee_Management_API_ASP.NET_Core.Services
{
    public interface IEmployeeService
    {
        Task<object> GetEmployeeBasicInfo(int EmpId);
    }
}