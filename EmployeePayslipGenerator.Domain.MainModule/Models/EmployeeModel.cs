namespace EmployeePayslipGenerator.Domain.MainModule.Models
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public decimal AnnualSalary { get; set; }
    }
}
