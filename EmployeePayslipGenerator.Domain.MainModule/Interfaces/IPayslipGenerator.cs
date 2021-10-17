using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Interfaces
{
    public interface IPayslipGenerator
    {
        string GeneratePayslip(EmployeeModel employee);
    }
}
