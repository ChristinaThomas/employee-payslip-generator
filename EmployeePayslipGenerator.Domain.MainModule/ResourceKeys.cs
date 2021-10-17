namespace EmployeePayslipGenerator.Domain.MainModule
{
    public static class ResourceKeys
    {
        public const string FirstNameBlank = "Please enter First Name.";
        public const string FirstNameMaxLength = "First name length cannot exceed 50 characters.";

        public const string LastNameBlank = "Please enter Last Name.";
        public const string LastNameMaxLength = "Last name length cannot exceed 50 characters.";

        public const string SalaryNotValidNumber = "Please enter valid Annual Salary.";
        public const string SalaryLessThanZero = "Salary cannot be less than zero.";
        public const string SalaryMaxLength = "Salary length cannot exceed 20 digits.";
        public const string SalaryDecimalPlaces = "Salary cannot exceed 2 decimal places";

        public const string EmployeeObjectNullExceptionMessage = "Employee object is null.";
    }
}
