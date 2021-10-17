using System;
using System.Globalization;

namespace EmployeePayslipGenerator.Domain.MainModule.Helpers
{
    public static class Functions
    {
        public static bool IsNumeric(object value)
        {
            bool isNumeric = value != null && Double.TryParse(Convert.ToString(value), NumberStyles.Any, NumberFormatInfo.InvariantInfo, result: out _);
            return isNumeric;
        }

        public static decimal RoundUp(decimal value, int numberOfDecimalPlaces)
        {
            return Math.Round(value, numberOfDecimalPlaces, MidpointRounding.AwayFromZero);
        }
    }
}
