# Employee Payslip Generator
Employee Payslip Generator is a simple, maintainable, easy to use application that can generate employee's monthly payslip based on annual salary.

![Alt text](https://github.com/ChristinaThomas/epg-screenshots/blob/main/employee-payslip-generator-app.PNG)

## Table of Contents  
- [Installation](#installation)  
- [Built Using](#built-using)
- [Prerequisites](#prerequisites)
- [Assumptions-TradeOffs](#assumptions-tradeOffs)
- [Design Considerations](#design-considerations)
- [Unit tests](#unit-tests)

## Installation
The Employee Payslip Generator executable can be downloaded from [Releases](https://github.com/ChristinaThomas/employee-payslip-generator/releases/tag/1.0) section.

Please ensure that you have the required permissions to run the [Employee Payslip Generator](https://github.com/ChristinaThomas/epg-screenshots/blob/main/Executable.png) executable.

## Built Using
 - Language : C#
 - Project Type : .Net Core Console Application
 - IDE : Visual Studio 2019
 
 ## Prerequisites
 In order to view/run the source code in development environment, following are the pre-requisites:
 - Visual Studio 2019 (preferred)
 - .Net 5.0 SDK
 
 ## Assumptions-TradeOffs
 - Tax Bracket details may or may not change for a financial year. In real time situation, the tax bracket data would either be retrieved from database or any other external source.
 - Employee names can contain alphabets, special characters or numbers as there are no universal rules to validate names. The same is being followed in most of the popular websites.
   E.g. Gmail, Yahoo. 
 - Salary, deductions, net income, gross monthly income values are limited/rounded to 2 decimal places.
  
## Design Considerations
- The system was built
  - to ensure it abides with the SOLID principles and the future requirements can be implemented easily by extending the code with minimal impacts.
  - using **Dependency Injection** pattern to achieve the following goals
    - reduce class coupling
    - increase code reusability
    - improve code maintainability
 - The "Employee" class was not created on purpose at this stage as there are no actions to be performed by the Employee. The class can be created in future based on the requirement.
 - The system is capable of handling multiple inputs in addition to name and salary inputs due to the fact that the values are stored in a model which in turn is passed to the business layer 
   instead of passing individual parameters as inputs.
 - Diagrammatic representation:
   ![Alt text](https://github.com/ChristinaThomas/epg-screenshots/blob/main/data-flow-diagram.PNG)
   
## Unit tests
- Unit tests are written using Nunit v3.13.1 to cover every scenario thereby supporting TDD approach.
- Running .Net Core Nunit tests using Nunit command line isn't supported yet and hence may need to be executed using Visual Studio.
- Steps to execute Nunit Unit tests on Visual Studio 2019 IDE:
  - Click on the **Test** menu on Visual Studio and select the **Test Explorer**
  - Run all tests
  ![Alt text](https://github.com/ChristinaThomas/epg-screenshots/blob/main/nunit-unit-tests.PNG)

 
  
  
 
