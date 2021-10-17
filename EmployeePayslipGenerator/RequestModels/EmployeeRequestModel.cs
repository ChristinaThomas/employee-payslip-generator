using System;
using System.ComponentModel.DataAnnotations;
using EmployeePayslipGenerator.Domain.MainModule;
using EmployeePayslipGenerator.Domain.MainModule.CustomValidators;
using EmployeePayslipGenerator.Domain.MainModule.Models;

namespace EmployeePayslipGenerator.RequestModels
{
    public class EmployeeRequestModel
    {
        [Required(ErrorMessage = ResourceKeys.FirstNameBlank)]
        [MaxLength(50, ErrorMessage = ResourceKeys.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ResourceKeys.LastNameBlank)]
        [MaxLength(50, ErrorMessage = ResourceKeys.LastNameMaxLength)]
        public string LastName { get; set; }

        [MaxLength(20, ErrorMessage = ResourceKeys.SalaryMaxLength)]
        [IsNumeric(ErrorMessage = ResourceKeys.SalaryNotValidNumber)]
        [DecimalPlaces(2,ErrorMessage = ResourceKeys.SalaryDecimalPlaces)]
        [IsNumberNotLessThanZeroAttribute(ErrorMessage = ResourceKeys.SalaryLessThanZero)]
        public string AnnualSalary { get; set; }


        public static EmployeeModel MapTo(EmployeeRequestModel employeeRequestModel)
        {
            return new EmployeeModel()
            {
                FirstName = employeeRequestModel.FirstName,
                LastName = employeeRequestModel.LastName,
                AnnualSalary = Convert.ToDecimal(employeeRequestModel.AnnualSalary)
            };
        }

    }
}
