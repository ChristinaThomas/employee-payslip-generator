using System.Collections.Generic;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.Domain.MainModule.Services
{
    public class TaxBracketService : ITaxBracketService
    {

        /// <summary>
        /// This method is created with an intention to retrieve the bracket information either from database or from any other
        /// storage as these tax brackets may or may not change for a financial year. The tax bracket values are currently hardcoded.
        /// </summary>
        public List<TaxBracketModel> GetTaxBracketDetails()
        {
            var firstBracket = new TaxBracketModel()
            {
                MinimumIncome = 0,
                MaximumIncome = 20000,
                MinimumTaxApplicable = 0
            };
            var secondBracket = new TaxBracketModel()
            {
                MinimumIncome = 20001,
                MaximumIncome = 40000,
                MinimumTaxApplicable = 0.1m
            };
            var thirdBracket = new TaxBracketModel()
            {
                MinimumIncome = 40001,
                MaximumIncome = 80000,
                MinimumTaxApplicable = 0.2m
            };
            var fourthBracket = new TaxBracketModel()
            {
                MinimumIncome = 80001,
                MaximumIncome = 180000,
                MinimumTaxApplicable = 0.3m
            };
            var fifthBracket = new TaxBracketModel()
            {
                MinimumIncome = 180001,
                MaximumIncome = -1,
                MinimumTaxApplicable = 0.4m
            };

            var currentFinancialTaxBracketList = new List<TaxBracketModel>()
            {
                firstBracket, secondBracket, thirdBracket, fourthBracket, fifthBracket
            };

            return currentFinancialTaxBracketList;
        }
    }
}
