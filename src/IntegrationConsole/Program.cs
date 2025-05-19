// See https://aka.ms/new-console-template for more information
using IntegrationConsole;
using System.Diagnostics;
using System.Text.Json;

Console.WriteLine("Hello, World!");
IDepartment department= new Department_null();
department.Name = "IT";
IEmployee employee = new Employee_null();
employee.FirstName = "Andrei";
employee.Department=department;
Console.WriteLine(employee.FirstName);
Console.WriteLine(employee.Department.Name);

//serialize and deserialize
var empString = JsonSerializer.Serialize(employee);
Console.WriteLine(empString);
//deserialize

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    DefaultBufferSize = 128
};
options.Converters.Add(new IDepartmentConverter());
options.Converters.Add(new IEmployeeConverter());

var emp2 = JsonSerializer.Deserialize<IEmployee>(empString,options);
ArgumentNullException.ThrowIfNull(emp2);
Console.WriteLine(emp2.FirstName);
Console.WriteLine(emp2.Department.Name);
Debug.Assert(emp2.FirstName == "Andrei");
foreach (var item in department.Employees())
{
    Console.WriteLine(item.FirstName);
}
await foreach (var item in department.EmployeesAsync())
{
    Console.WriteLine(item.FirstName);
}

Manager_null manager = new Manager_null();
manager.FirstName = "Andrei";
var manager2 = new Manager_null();
manager2.CopyPropertiesFrom(manager);


if(manager2.FirstName != manager.FirstName)
{
    throw new Exception("CopyPropertiesFrom failed");
}

if(!manager2.PropertiesAreEqual(manager))
{
    throw new Exception("PropertiesAreEqual failed");
}
Console.WriteLine(manager2.FirstName);
