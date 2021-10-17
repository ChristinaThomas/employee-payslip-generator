using System;
using System.Text;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Services
{
    public class MonthlyPayslipGeneratorService : IPayslipGenerator
    {
        private readonly ISalaryCalculator _salaryCalculator;

        public MonthlyPayslipGeneratorService(ISalaryCalculator salaryCalculator)
        {
            _salaryCalculator = salaryCalculator;
        }

        public string GeneratePayslip(EmployeeModel employee)
        {
            if (employee == null)
                throw new ArgumentNullException(ResourceKeys.EmployeeObjectNullExceptionMessage);

            var salaryModel = _salaryCalculator.GetSalary(employee);
            return PrintMonthlyPayslip(salaryModel, employee);
        }

        private string PrintMonthlyPayslip(SalaryModel salaryModel, EmployeeModel employee)
        {
            var payslip = new StringBuilder();

            payslip.AppendLine();
            payslip.AppendFormat("Monthly Payslip for: {0} ", employee?.FullName);
            payslip.AppendLine();
            payslip.AppendFormat("Gross Monthly Income: ${0}", salaryModel?.GrossMonthlyIncome);
            payslip.AppendLine();
            payslip.AppendFormat("Monthly Income Tax: ${0}", salaryModel?.MonthlyTaxDeductions);
            payslip.AppendLine();
            payslip.AppendFormat("Net Monthly Income: ${0}", salaryModel?.NetMonthlyIncome);

            return payslip.ToString();
        }
    }
}
