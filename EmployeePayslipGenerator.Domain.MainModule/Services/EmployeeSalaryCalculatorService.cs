using System;
using System.ComponentModel.DataAnnotations;
using EmployeePayslipGenerator.Domain.MainModule.Helpers;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Services
{
    public class EmployeeSalaryCalculatorService : ISalaryCalculator
    {
        private const int AnnualMonths = 12;
        private readonly IDeduction _deduction;

        public EmployeeSalaryCalculatorService(IDeduction deduction)
        {
            _deduction = deduction;
        }

        public SalaryModel GetSalary(EmployeeModel employee)
        {
            if (employee == null)
                throw new ArgumentNullException(ResourceKeys.EmployeeObjectNullExceptionMessage);

            SalaryModel salaryModel;

            if (employee.AnnualSalary >= 0)
            {
                var grossMonthlyIncome = GetGrossMonthlyIncome(employee.AnnualSalary);
                var deductions = GetDeductions(employee);
                var netMonthlyIncome = grossMonthlyIncome - deductions?.MonthlyIncomeTaxDeductions ??
                                       grossMonthlyIncome;

                salaryModel = new SalaryModel()
                {
                    NetMonthlyIncome = netMonthlyIncome,
                    GrossMonthlyIncome = grossMonthlyIncome,
                    MonthlyTaxDeductions = deductions?.MonthlyIncomeTaxDeductions ?? 0
                };
            }
            else
            {
                throw new ValidationException(ResourceKeys.SalaryLessThanZero);
            }

            return salaryModel;
        }

        private decimal GetGrossMonthlyIncome(decimal annualSalary)
        {
            decimal grossMonthlyIncome = Functions.RoundUp((annualSalary / AnnualMonths), 2);

            return grossMonthlyIncome;
        }

        private DeductionsModel GetDeductions(EmployeeModel employee)
        {
            DeductionsModel deductions = null;
            if (_deduction != null)
            {
                deductions = _deduction.GetDeductions(employee);
            }

            return deductions;
        }

    }
}
