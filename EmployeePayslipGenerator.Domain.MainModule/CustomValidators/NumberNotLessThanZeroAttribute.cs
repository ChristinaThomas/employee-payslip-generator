using System;
using System.ComponentModel.DataAnnotations;
using EmployeePayslipGenerator.Domain.MainModule.Helpers;

namespace EmployeePayslipGenerator.Domain.MainModule.CustomValidators
{
    public class IsNumberNotLessThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Functions.IsNumeric(value) && Convert.ToDouble(value) < 0)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
