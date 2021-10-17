using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Models;
using EmployeePayslipGenerator.Domain.MainModule.Services;
using EmployeePayslipGenerator.RequestModels;
using Moq;
using NUnit.Framework;

namespace EmployeePayslipGenerator.Test
{
    [TestFixture]
    public class SalaryCalculatorTest
    {
        private readonly IServiceProvider _serviceProvider;
        public SalaryCalculatorTest()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var taxBracketService = new TaxBracketService();
            var incomeTaxDeductionService = new IncomeTaxDeductionService(taxBracketService);

            serviceProvider.Setup(x => x.GetService(typeof(ISalaryCalculator)))
                           .Returns(new EmployeeSalaryCalculatorService(incomeTaxDeductionService));
   
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);

            _serviceProvider = serviceProvider.Object;
        }

        [Test]
        public void Test_Get_Calculated_Salary_In_First_TaxBracket()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "10000"
            };

            var employee = EmployeeRequestModel.MapTo(employeeRequestModel);
            Assert.IsNotNull(employee);
            employee.FirstName = employeeRequestModel.FirstName;
            employee.LastName = employeeRequestModel.LastName;
            employee.AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(employeeRequestModel.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(employeeRequestModel.FirstName, employee.FirstName);
            Assert.AreEqual(employeeRequestModel.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            var salaryModel = salaryCalculatorService.GetSalary(employee);
            Assert.IsNotNull(salaryModel);

            Assert.AreEqual(833.33, salaryModel.GrossMonthlyIncome);
            Assert.AreEqual(0, salaryModel.MonthlyTaxDeductions);
            Assert.AreEqual(833.33, salaryModel.NetMonthlyIncome);
        }

        [Test]
        public void Test_Get_Calculated_Salary_In_Second_TaxBracket()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "20001"
            };

            var employee = EmployeeRequestModel.MapTo(employeeRequestModel);
            Assert.IsNotNull(employee);
            employee.FirstName = employeeRequestModel.FirstName;
            employee.LastName = employeeRequestModel.LastName;
            employee.AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(employeeRequestModel.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(employeeRequestModel.FirstName, employee.FirstName);
            Assert.AreEqual(employeeRequestModel.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            var salaryModel = salaryCalculatorService.GetSalary(employee);
            Assert.IsNotNull(salaryModel);

            Assert.AreEqual(1666.75m, salaryModel.GrossMonthlyIncome);
            Assert.AreEqual(0.01m, salaryModel.MonthlyTaxDeductions);
            Assert.AreEqual(1666.74m, salaryModel.NetMonthlyIncome);
        }

        [Test]
        public void Test_Get_Calculated_Salary_In_Third_TaxBracket()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "60000"
            };

            var employee = EmployeeRequestModel.MapTo(employeeRequestModel);
            Assert.IsNotNull(employee);
            employee.FirstName = employeeRequestModel.FirstName;
            employee.LastName = employeeRequestModel.LastName;
            employee.AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(employeeRequestModel.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(employeeRequestModel.FirstName, employee.FirstName);
            Assert.AreEqual(employeeRequestModel.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            var salaryModel = salaryCalculatorService.GetSalary(employee);
            Assert.IsNotNull(salaryModel);

            Assert.AreEqual(5000, salaryModel.GrossMonthlyIncome);
            Assert.AreEqual(500, salaryModel.MonthlyTaxDeductions);
            Assert.AreEqual(4500, salaryModel.NetMonthlyIncome);
        }

        [Test]
        public void Test_Get_Calculated_Salary_In_Fourth_TaxBracket()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "100000"
            };

            var employee = EmployeeRequestModel.MapTo(employeeRequestModel);
            Assert.IsNotNull(employee);
            employee.FirstName = employeeRequestModel.FirstName;
            employee.LastName = employeeRequestModel.LastName;
            employee.AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(employeeRequestModel.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(employeeRequestModel.FirstName, employee.FirstName);
            Assert.AreEqual(employeeRequestModel.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            var salaryModel = salaryCalculatorService.GetSalary(employee);
            Assert.IsNotNull(salaryModel);

            Assert.AreEqual(8333.33m, salaryModel.GrossMonthlyIncome);
            Assert.AreEqual(1333.33m, salaryModel.MonthlyTaxDeductions);
            Assert.AreEqual(7000, salaryModel.NetMonthlyIncome);
        }

        [Test]
        public void Test_Get_Calculated_Salary_In_Fifth_TaxBracket()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "200000"
            };

            var employee = EmployeeRequestModel.MapTo(employeeRequestModel);
            Assert.IsNotNull(employee);
            employee.FirstName = employeeRequestModel.FirstName;
            employee.LastName = employeeRequestModel.LastName;
            employee.AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(employeeRequestModel.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(employeeRequestModel.FirstName, employee.FirstName);
            Assert.AreEqual(employeeRequestModel.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            var salaryModel = salaryCalculatorService.GetSalary(employee);
            Assert.IsNotNull(salaryModel);

            Assert.AreEqual(16666.67m, salaryModel.GrossMonthlyIncome);
            Assert.AreEqual(4000, salaryModel.MonthlyTaxDeductions);
            Assert.AreEqual(12666.67, salaryModel.NetMonthlyIncome);
        }

        [Test]
        public void Test_Get_Calculated_Salary_In_Negative_AnnualSalary()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "-200"
            };

            var employee = EmployeeRequestModel.MapTo(employeeRequestModel);
            Assert.IsNotNull(employee);
            employee.FirstName = employeeRequestModel.FirstName;
            employee.LastName = employeeRequestModel.LastName;
            employee.AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary);

            Assert.IsNotNull(employee);
            Assert.AreEqual(Convert.ToDecimal(employeeRequestModel.AnnualSalary), employee.AnnualSalary);
            Assert.AreEqual(employeeRequestModel.FirstName, employee.FirstName);
            Assert.AreEqual(employeeRequestModel.LastName, employee.LastName);
            Assert.AreEqual($"{employee.FirstName} {employee.LastName}", employee.FullName);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            Assert.Throws<ValidationException>(() => salaryCalculatorService.GetSalary(employee));
        }

        [Test]
        public void Test_Get_Calculated_Salary_EmployeeObject_Is_Null()
        {

            EmployeeModel employee = null;
            Assert.IsNull(employee);

            var salaryCalculatorService = _serviceProvider.GetService<ISalaryCalculator>();
            Assert.IsNotNull(salaryCalculatorService);

            Assert.Throws<ArgumentNullException>(() => salaryCalculatorService.GetSalary(employee));
        }
    }
}
