// See https://aka.ms/new-console-template for more information
using IntegrationConsole;

Console.WriteLine("Hello, World!");
IDepartment department= new Department_null();
Console.WriteLine(department.Employees.Length);
department.Name = "IT";
IEmployee employee = new Employee_null();
employee.FirstName = "Andrei";
employee.Department=department;
Console.WriteLine(employee.FirstName);
Console.WriteLine(employee.Department.Name);