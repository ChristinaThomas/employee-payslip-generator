using System;
using System.Text;
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
    public class PayslipGeneratorTest
    {
        private readonly IServiceProvider _serviceProvider;
        public PayslipGeneratorTest()
        {
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
        public void Test_Payslip_Generation_Valid_Input()
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

            var payslipGeneratorService = _serviceProvider.GetService<IPayslipGenerator>();
            Assert.IsNotNull(payslipGeneratorService);

            var payslip = payslipGeneratorService.GeneratePayslip(employee);
            Assert.IsTrue(!string.IsNullOrEmpty(payslip));

            var mockPayslip = GetMockPayslip(employee);
            Assert.IsTrue(!string.IsNullOrEmpty(mockPayslip));

           Assert.AreEqual(mockPayslip, payslip);
        }

        [Test]
        public void Test_Payslip_Generation_Employee_Object_Null()
        {
            EmployeeModel employee = null;
            Assert.IsNull(employee);

            var payslipGenerator = _serviceProvider.GetService<IPayslipGenerator>();
            Assert.IsNotNull(payslipGenerator);

            Assert.Throws<ArgumentNullException>(() => payslipGenerator.GeneratePayslip(employee));
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
