using InterfaceToNullObject;

namespace IntegrationConsole;
[ToNullObject]
public interface IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IDepartment Department { get; set; }
    public string GetFullName();

    public string GetFullNameAndDepartment(string separator);

}
