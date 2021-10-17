namespace EmployeePayslipGenerator.Domain.MainModule.Models
{
    public class TaxBracketModel
    {
        public int MinimumIncome { get; set; }
        public int MaximumIncome { get; set; }
        public decimal MinimumTaxApplicable { get; set; }
    }
}
