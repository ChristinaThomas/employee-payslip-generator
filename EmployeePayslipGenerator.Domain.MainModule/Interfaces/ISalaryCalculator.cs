using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Interfaces
{
    public interface ISalaryCalculator
    {
        SalaryModel GetSalary(EmployeeModel employee);
    }
}
