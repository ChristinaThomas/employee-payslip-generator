using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EmployeePayslipGenerator.Domain.MainModule;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Models;
using EmployeePayslipGenerator.Domain.MainModule.Services;
using EmployeePayslipGenerator.RequestModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace EmployeePayslipGenerator.Test
{
    [TestFixture]
    public class UserInputValidationTest
    {
        private readonly EmployeePayrollStartupService _startupService;
        private readonly IServiceProvider _serviceProvider;

        public UserInputValidationTest()
        {
            _startupService = new EmployeePayrollStartupService();
            var serviceProvider = new Mock<IServiceProvider>();
            var taxBracketService = new TaxBracketService();
            var incomeTaxDeductionService = new IncomeTaxDeductionService(taxBracketService);
            var salaryCalculatorService = new EmployeeSalaryCalculatorService(incomeTaxDeductionService);

            serviceProvider.Setup(x => x.GetService(typeof(ISalaryCalculator)))
                .Returns(new EmployeeSalaryCalculatorService(incomeTaxDeductionService));

            serviceProvider.Setup(x => x.GetService(typeof(IPayslipGenerator)))
                .Returns(new MonthlyPayslipGeneratorService(salaryCalculatorService));

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);

            _serviceProvider = serviceProvider.Object;
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Correct_Data()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "60000"
            };
            var employee = EmployeeRequestModel.MapTo(userEnteredData);
            Assert.IsNotNull(employee);
            employee.FirstName = userEnteredData.FirstName;
            employee.LastName = userEnteredData.LastName;
            employee.AnnualSalary = Convert.ToDecimal(userEnteredData.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(userEnteredData.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(userEnteredData.FirstName, employee.FirstName);
            Assert.AreEqual(userEnteredData.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var payslip = _startupService.ExecuteProcess(userEnteredData, _serviceProvider);
            Assert.AreEqual(payslip, GetMockPayslip(employee));
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Incorrect_Details()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "D'Souza",
                LastName = "De Mello",
                AnnualSalary = "sdf234"
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.SalaryNotValidNumber);
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_FirstName_Max_Length()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "ThisIsJustToTestTheMaxLengthOfThisFieldThatItDoesNotExceedFiftyCharactersLimitation",
                LastName = "Richards",
                AnnualSalary = "1234"
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.FirstNameMaxLength);
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_LastName_Max_Length()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "ThisIsJustToTestTheMaxLengthOfThisFieldThatItDoesNotExceedFiftyCharactersLimitation",
                AnnualSalary = "1234"
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.LastNameMaxLength);
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Blank_Salary()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                AnnualSalary = ""
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.SalaryNotValidNumber);

        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Salary_Less_Than_Zero()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "-20"
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.SalaryLessThanZero);
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Salary_Up_to_Two_Decimals()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "4000.20"
            };

            var employee = EmployeeRequestModel.MapTo(userEnteredData);
            Assert.IsNotNull(employee);
            employee.FirstName = userEnteredData.FirstName;
            employee.LastName = userEnteredData.LastName;
            employee.AnnualSalary = Convert.ToDecimal(userEnteredData.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(userEnteredData.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(userEnteredData.FirstName, employee.FirstName);
            Assert.AreEqual(userEnteredData.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var payslip = _startupService.ExecuteProcess(userEnteredData, _serviceProvider);
            Assert.AreEqual(payslip, GetMockPayslip(employee));
        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Salary_More_Than_to_Two_Decimals()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "4000.20234234"
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.SalaryDecimalPlaces);

        }

        [Test]
        public void Test_User_Entered_Employee_Details_Validation_Salary_Max_Length()
        {
            EmployeeRequestModel userEnteredData = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "4000234324234234234.20234234"
            };

            Assert.Throws<ValidationException>(() => _startupService.ExecuteProcess(userEnteredData, _serviceProvider), ResourceKeys.SalaryMaxLength);

        }

        private string GetMockPayslip(EmployeeModel employee)
        {
            var payslip = new StringBuilder();

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            if (salaryCalculatorService != null)
            {
                var salaryModel = salaryCalculatorService.GetSalary(employee);

                payslip.AppendLine();
                payslip.AppendFormat("Monthly Payslip for: {0} ", employee?.FullName);
                payslip.AppendLine();
                payslip.AppendFormat("Gross Monthly Income: ${0}", salaryModel?.GrossMonthlyIncome);
                payslip.AppendLine();
                payslip.AppendFormat("Monthly Income Tax: ${0}", salaryModel?.MonthlyTaxDeductions);
                payslip.AppendLine();
                payslip.AppendFormat("Net Monthly Income: ${0}", salaryModel?.NetMonthlyIncome);
            }

            return payslip.ToString();
        }
    }
}