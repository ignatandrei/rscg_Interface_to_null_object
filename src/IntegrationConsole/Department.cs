
using InterfaceToNullObject;


namespace IntegrationConsole;
[ToNullObject()]
public interface IDepartment
{
    public string Name { get; set; }

    public IEmployee[] Employees();
    public IAsyncEnumerable<IEmployee> EmployeesAsync();

}
