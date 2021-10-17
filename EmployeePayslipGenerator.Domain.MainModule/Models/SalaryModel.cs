namespace EmployeePayslipGenerator.Domain.MainModule.Models
{
    public class SalaryModel
    {
        public decimal NetMonthlyIncome { get; set; }
        public decimal GrossMonthlyIncome { get; set; }
        public decimal MonthlyTaxDeductions { get; set; }
    }
}
