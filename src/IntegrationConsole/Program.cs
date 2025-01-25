// See https://aka.ms/new-console-template for more information
using IntegrationConsole;

Console.WriteLine("Hello, World!");
IDepartment department= new Department_null();
department.Name = "IT";
IEmployee employee = new Employee_null();
employee.FirstName = "Andrei";
employee.Department=department;
Console.WriteLine(employee.FirstName);
Console.WriteLine(employee.Department.Name);
foreach (var item in department.Employees())
{
    Console.WriteLine(item.FirstName);
}
await foreach (var item in department.EmployeesAsync())
{
    Console.WriteLine(item.FirstName);
}