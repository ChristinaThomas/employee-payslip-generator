using System;
using System.ComponentModel.DataAnnotations;
using EmployeePayslipGenerator.RequestModels;
using Microsoft.Extensions.DependencyInjection;
using EmployeePayslipGenerator.Domain.MainModule.Interfaces;
using EmployeePayslipGenerator.Domain.MainModule.Services;

namespace EmployeePayslipGenerator
{
    public class EmployeePayslipGenerator
    {
        public static void Main(string[] args)
        {
            EmployeePayrollStartupService startupService = new EmployeePayrollStartupService();
            var serviceProvider = RegisterServices();
            try
            {
                string shouldContinue;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("*********GenerateMonthlyPayslip*********");
                    Console.Write("First Name: ");
                    var firstName = Console.ReadLine();

                    Console.Write("Last Name: ");
                    var lastName = Console.ReadLine();

                    Console.Write("Annual Salary: ");
                    var salary = Console.ReadLine();


                    var employeeDetailsAsInput = new EmployeeRequestModel()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        AnnualSalary = salary
                    };

                    try
                    {
                        var payslip = startupService.ExecuteProcess(employeeDetailsAsInput, serviceProvider);
                        Console.WriteLine(payslip);
                        Console.WriteLine();
                    }
                    catch (ValidationException validationException)
                    {
                        Console.WriteLine(validationException.Message);
                    }

                    Console.Write("Please press Enter to continue or N to exit ");
                  
                    shouldContinue = Console.ReadLine();
                } while (!shouldContinue.ToLower().Equals("n"));
            }

            catch (Exception e)
            {
                Console.WriteLine("Something went wrong!!. Error: " + e.Message);
            }
        }

        private static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IPayslipGenerator, MonthlyPayslipGeneratorService>();
            services.AddSingleton<ISalaryCalculator, EmployeeSalaryCalculatorService>();
            services.AddSingleton<IDeduction, IncomeTaxDeductionService>();
            services.AddSingleton<ITaxBracketService, TaxBracketService>();

            return services.BuildServiceProvider(true);
        }

    }
}
