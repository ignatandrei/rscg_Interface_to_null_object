using InterfaceToNullObject;

namespace IntegrationConsole;
[ToNullObject]
internal interface IManager: IEmployee
{
    public bool MoveEmployeeToDepartment(IEmployee emp, IDepartment dep);

    public IEmployee[] GetAllEmployees();

}
