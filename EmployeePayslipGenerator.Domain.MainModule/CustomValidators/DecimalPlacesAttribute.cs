using System;
using System.ComponentModel.DataAnnotations;
using EmployeePayslipGenerator.Domain.MainModule.Helpers;

namespace EmployeePayslipGenerator.Domain.MainModule.CustomValidators
{
    public class DecimalPlacesAttribute : ValidationAttribute
    {
        private readonly int _decimalPlaces;
        public DecimalPlacesAttribute(int decimalPlaces)
        {
            _decimalPlaces = decimalPlaces;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && Functions.IsNumeric(value))
            {
                var decimalNumbersAfterSeparator = Convert.ToString(value)?.Split(new[] { '.' });
                if (decimalNumbersAfterSeparator != null &&
                    decimalNumbersAfterSeparator.Length >= 2 &&
                    decimalNumbersAfterSeparator[1].Length > _decimalPlaces)
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
