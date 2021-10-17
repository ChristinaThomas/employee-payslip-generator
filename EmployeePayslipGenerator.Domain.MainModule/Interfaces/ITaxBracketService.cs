using System.Collections.Generic;
using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Interfaces
{
    public interface ITaxBracketService
    {
        List<TaxBracketModel> GetTaxBracketDetails();
    }
}
