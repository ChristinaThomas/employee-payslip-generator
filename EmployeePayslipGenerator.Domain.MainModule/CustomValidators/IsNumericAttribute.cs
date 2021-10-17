using System.ComponentModel.DataAnnotations;
using EmployeePayslipGenerator.Domain.MainModule.Helpers;

namespace EmployeePayslipGenerator.Domain.MainModule.CustomValidators
{
    public class IsNumericAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !Functions.IsNumeric(value))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
