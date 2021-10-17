using EmployeePayslipGenerator.Domain.MainModule.Helpers;
using NUnit.Framework;

namespace EmployeePayslipGenerator.Test
{
    [TestFixture]
    public class HelpersTest
    {
        [Test]
        public void Test_IsNumeric_Helper_Method()
        {
            var inputString1 = "123";
            Assert.IsTrue(Functions.IsNumeric(inputString1));

            var inputString2 = "abc23";
            Assert.IsFalse(Functions.IsNumeric(inputString2));

            var inputString3 = "123.12";
            Assert.IsTrue(Functions.IsNumeric(inputString3));

            var inputString4 = "sdf!@#";
            Assert.IsFalse(Functions.IsNumeric(inputString4));
        }

        [Test]
        public void Test_RoundUp_Helper_Method()
        {
            var input1 = 123;
            Assert.AreEqual(123, Functions.RoundUp(input1, 2));

            var input2 = 123.233m;
            Assert.AreEqual(123.23m, Functions.RoundUp(input2, 2));

            var input3 = 123.456m;
            Assert.AreEqual(123.46m, Functions.RoundUp(input3, 2));
        }
    }
}
