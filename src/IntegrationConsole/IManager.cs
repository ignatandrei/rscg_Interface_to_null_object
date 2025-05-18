﻿using InterfaceToNullObject;

namespace IntegrationConsole;
[ToNullObject]
public interface IManager : IEmployee
{
    public bool MoveEmployeeToDepartment(IEmployee emp, IDepartment dep);

    public IEmployee[] GetAllEmployees { get; set; }

}