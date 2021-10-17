using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Interfaces
{
    public interface IDeduction
    {
        DeductionsModel GetDeductions(EmployeeModel employee);
    }
}
