using System;
using Microsoft.Extensions.DependencyInjection;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Services;
using Moq;
using NUnit.Framework;

namespace EmployeePayslipGenerator.Test
{
    [TestFixture]
    public class TaxBracketServiceTest
    {
        private readonly IServiceProvider _serviceProvider;
        public TaxBracketServiceTest()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider.Setup(x => x.GetService(typeof(ITaxBracketService)))
                .Returns(new TaxBracketService());

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            serviceProvider.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(serviceScopeFactory.Object);

            _serviceProvider = serviceProvider.Object;
        }

        [Test]
        public void Test_Get_TaxBracket_Details()
        {
            var taxBracketService = _serviceProvider.GetService<ITaxBracketService>();
            Assert.IsNotNull(taxBracketService);

            var taxBrackets = taxBracketService.GetTaxBracketDetails();
            Assert.IsNotNull(taxBrackets);
            Assert.IsTrue(taxBrackets.Count == 5);

            //First Tax Bracket
            Assert.AreEqual(0, taxBrackets[0].MinimumIncome);
            Assert.AreEqual(20000, taxBrackets[0].MaximumIncome);
            Assert.AreEqual(0, taxBrackets[0].MinimumTaxApplicable);

            //First Tax Bracket
            Assert.AreEqual(20001, taxBrackets[1].MinimumIncome);
            Assert.AreEqual(40000, taxBrackets[1].MaximumIncome);
            Assert.AreEqual(0.1m, taxBrackets[1].MinimumTaxApplicable);

            //Third Tax Bracket
            Assert.AreEqual(40001, taxBrackets[2].MinimumIncome);
            Assert.AreEqual(80000, taxBrackets[2].MaximumIncome);
            Assert.AreEqual(0.2m, taxBrackets[2].MinimumTaxApplicable);

            //Fourth Tax Bracket
            Assert.AreEqual(80001, taxBrackets[3].MinimumIncome);
            Assert.AreEqual(180000, taxBrackets[3].MaximumIncome);
            Assert.AreEqual(0.3m, taxBrackets[3].MinimumTaxApplicable);

            //Fifth Tax Bracket
            Assert.AreEqual(180001, taxBrackets[4].MinimumIncome);
            Assert.AreEqual(-1, taxBrackets[4].MaximumIncome);
            Assert.AreEqual(0.4m, taxBrackets[4].MinimumTaxApplicable);
        }
    }
}
