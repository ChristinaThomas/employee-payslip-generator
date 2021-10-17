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
    public class DeductionsTest
    {
        private readonly IServiceProvider _serviceProvider;
        public DeductionsTest ()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(IDeduction))).Returns(new IncomeTaxDeductionService
                                                                                                        (new TaxBracketService()));
        
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);

            _serviceProvider = serviceProvider.Object;
        }

        [Test]
        public void Test_Get_Deductions_Annual_Salary_In_First_TaxBracket()
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

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            var deductionModel = deduction.GetDeductions(employee);
            Assert.IsNotNull(deduction);

            Assert.AreEqual(0, deductionModel.MonthlyIncomeTaxDeductions);
        }

        [Test]
        public void Test_Get_Deductions_Annual_Salary_In_Second_TaxBracket()
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

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            var deductionModel = deduction.GetDeductions(employee);
            Assert.IsNotNull(deduction);

            Assert.AreEqual(0.01m, deductionModel.MonthlyIncomeTaxDeductions);
        }

        [Test]
        public void Test_Get_Deductions_Annual_Salary_In_Third_TaxBracket()
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

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            var deductionModel = deduction.GetDeductions(employee);
            Assert.IsNotNull(deduction);

            Assert.AreEqual(500, deductionModel.MonthlyIncomeTaxDeductions);
        }

        [Test]
        public void Test_Get_Deductions_Annual_Salary_In_Fourth_TaxBracket()
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

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            var deductionModel = deduction.GetDeductions(employee);
            Assert.IsNotNull(deduction);

            Assert.AreEqual(1333.33m, deductionModel.MonthlyIncomeTaxDeductions);
        }

        [Test]
        public void Test_Get_Deductions_Annual_Salary_In_Fifth_TaxBracket()
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

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            var deductionModel = deduction.GetDeductions(employee);
            Assert.IsNotNull(deduction);

            Assert.AreEqual(4000, deductionModel.MonthlyIncomeTaxDeductions);
        }

        [Test]
        public void Test_Get_Deductions_With_Negative_Annual_Salary()
        {
            EmployeeRequestModel employeeRequestModel = new EmployeeRequestModel()
            {
                FirstName = "Jeremy",
                LastName = "Richards",
                AnnualSalary = "-100"
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

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            Assert.Throws<ValidationException>(() => deduction.GetDeductions(employee));
        }

        [Test]
        public void Test_Get_Deductions_EmployeeObject_Is_Null()
        {

            EmployeeModel employee = null;
            Assert.IsNull(employee);

            var deduction = _serviceProvider.GetService<IDeduction>();
            Assert.IsNotNull(deduction);

            Assert.Throws<ArgumentNullException>(() => deduction.GetDeductions(employee));
        }
    }
}
