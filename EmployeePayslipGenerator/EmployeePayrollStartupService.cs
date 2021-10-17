using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using EmployeePayslipGenerator.Domain.MainModule.Helpers;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.RequestModels;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeePayslipGenerator
{
    public class EmployeePayrollStartupService
    {
        public string ExecuteProcess(EmployeeRequestModel employeeDetailsAsInput, IServiceProvider serviceProvider)
        {
            string payslip=string.Empty;
            var errors = ValidateInputData(employeeDetailsAsInput);

            if (errors != null && errors.Any())
            {
                var validationErrorsFormattedString = GetFormattedValidationErrorString(errors);
                throw new ValidationException(validationErrorsFormattedString);
            }
            else
            {
                var employee = EmployeeRequestModel.MapTo(employeeDetailsAsInput);

                if (serviceProvider != null)
                {
                    var payslipGenerator = serviceProvider.GetService<IPayslipGenerator>();
                    payslip = payslipGenerator?.GeneratePayslip(employee);
                }
            }

            return payslip;
        }

        private List<string> ValidateInputData(EmployeeRequestModel employeeDetailsAsInput)
        {
            var errorMessages = new List<string>();
            bool isValidInput = CustomInputValidationService.TryValidateFields(employeeDetailsAsInput, out ICollection<ValidationResult> validationResults);

            if (!isValidInput)
            {
                foreach (ValidationResult res in validationResults)
                {
                    errorMessages.Add(res.ErrorMessage);
                }
            }

            return errorMessages;
        }

        private static string GetFormattedValidationErrorString(List<string> errors)
        {
            StringBuilder errorStringBuilder = new StringBuilder();

            errorStringBuilder.AppendLine();

            if (errors.Any())
            {
                foreach (var message in errors)
                {
                    errorStringBuilder.AppendLine(message);
                }
            }

            return errorStringBuilder.ToString();
        }
    }
}
