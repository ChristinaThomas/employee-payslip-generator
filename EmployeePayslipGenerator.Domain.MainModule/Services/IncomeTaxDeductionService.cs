using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EmployeePayslipGenerator.Domain.MainModule.Helpers;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Services
{
    public class IncomeTaxDeductionService : IIncomeTaxDeduction
    {
        private const int AnnualMonths = 12;
        private readonly ITaxBracketService _taxBracketService;

        public IncomeTaxDeductionService(ITaxBracketService taxBracketService)
        {
            _taxBracketService = taxBracketService;
        }

        public DeductionsModel GetDeductions(EmployeeModel employee)
        {
            if (employee == null)
                throw new ArgumentNullException(ResourceKeys.EmployeeObjectNullExceptionMessage);

            DeductionsModel deductionsModel = new DeductionsModel
            {
                MonthlyIncomeTaxDeductions = GetCalculatedMonthlyIncomeTax(employee)
            };

            return deductionsModel;
        }

        private decimal GetCalculatedMonthlyIncomeTax(EmployeeModel employee)
        {
            decimal calculatedYearlyIncomeTax = 0;

            var taxBracketList = _taxBracketService.GetTaxBracketDetails();

            if (employee != null && taxBracketList != null && taxBracketList.Any())
            {
                if (employee.AnnualSalary >= 0)
                {
                    foreach (var taxBracket in taxBracketList)
                    {
                        if (employee.AnnualSalary >= taxBracket.MaximumIncome && taxBracket.MaximumIncome != -1)
                        {
                            calculatedYearlyIncomeTax += (taxBracket.MaximumIncome - (taxBracket.MinimumIncome - 1)) *
                                                         taxBracket.MinimumTaxApplicable;
                        }
                        else if ((employee.AnnualSalary >= taxBracket.MinimumIncome &&
                                  employee.AnnualSalary <= taxBracket.MaximumIncome) ||
                                 (employee.AnnualSalary >= taxBracket.MinimumIncome && taxBracket.MaximumIncome == -1))
                        {
                            //MaximumIncome value -1 denotes the last bracket e.g 180001 and "OVER"
                            calculatedYearlyIncomeTax += (employee.AnnualSalary - (taxBracket.MinimumIncome - 1)) *
                                                                           taxBracket.MinimumTaxApplicable;

                        }
                    }
                }
                else
                {
                    throw new ValidationException(ResourceKeys.SalaryLessThanZero);
                }
            }

            decimal calculatedMonthlyIncomeTax = Functions.RoundUp((calculatedYearlyIncomeTax / AnnualMonths), 2);

            return calculatedMonthlyIncomeTax;
        }

    }
}
