using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeePayslipGenerator.Domain.MainModule.Helpers
{
    public class CustomInputValidationService
    {
        public static bool TryValidateFields(object value, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(value, null, null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(value, context, results, true);
        }
    }
}
