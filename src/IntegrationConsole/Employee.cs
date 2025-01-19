using InterfaceToNullObject;

namespace IntegrationConsole;
[ToNullObject]
internal interface IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IDepartment Department { get; set; }
    public string GetFullName();
  
}
