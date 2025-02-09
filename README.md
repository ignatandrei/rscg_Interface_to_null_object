[![NuGet version](https://img.shields.io/nuget/v/rscg_Interface_to_null_object.svg?style=flat-square)](https://www.nuget.org/packages/rscg_Interface_to_null_object)

[![NuGet version](https://img.shields.io/nuget/v/rscg_Interface_to_null_object_common.svg?style=flat-square)](https://www.nuget.org/packages/rscg_Interface_to_null_object_common)


# Interface to Null Object Pattern
Implementation of https://en.wikipedia.org/wiki/Null_object_pattern  from interface

# Installation

Add to your csproj file:

```xml
  <ItemGroup>
    <PackageReference Include="rscg_Interface_to_null_object" Version="2025.125.2045"  OutputItemType="Analyzer" ReferenceOutputAssembly="false"  />
    <PackageReference Include="rscg_Interface_to_null_object_common" Version="2025.125.2045" />
  </ItemGroup>
	<PropertyGroup>
		<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
		<CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)\GX</CompilerGeneratedFilesOutputPath>
	</PropertyGroup>
```

Or add the nuget packages rscg_Interface_to_null_object  and rscg_Interface_to_null_object_common

# Usage

## Simple usage
```csharp
[InterfaceToNullObject.ToNullObject]
public interface IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IDepartment Department { get; set; }
    public string GetFullName();
  
}
```

And then a C# class that implements the interface will be generated

```csharp
public partial class Employee_null : global::IntegrationConsole.IEmployee
{

        public virtual string FirstName { get; set; } = default(string);
    
        public virtual string LastName { get; set; } = default(string);
    
        public virtual IntegrationConsole.IDepartment Department { get; set; } = default(IntegrationConsole.IDepartment);
    
        public virtual string GetFullName() { return default(string); }
    
}

```
## Deserialize to interface

See following code that deserializes to interface with a converter that is automatically generated

```csharp
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
```


## Adding default values

Let's say you want to return an empty string for the GetFullName method, you can add the following code to your csproj file

```xml
<ItemGroup>
  <CompilerVisibleProperty Include="I2NO_String" />
</ItemGroup>
<PropertyGroup>
  <I2NO_String>return ""</I2NO_String>	
</PropertyGroup>
```

So now the code will be generated like this

```csharp
public virtual string GetFullName() {  return "" ; }
```

For array and generics, see 
```xml
<ItemGroup>
	<CompilerVisibleProperty Include="I2NO_String" />
	<CompilerVisibleProperty Include="I2NO_IntegrationConsole_IEmployee_Array" />
	<CompilerVisibleProperty Include="I2NO_System_Collections_Generic_IAsyncEnumerable_Of_IntegrationConsole_IEmployee_EndOf" />
</ItemGroup>
<ItemGroup>
  <PackageReference Include="System.Linq.Async" Version="6.0.1" />
</ItemGroup>
<PropertyGroup>
	<I2NO_String>return ""</I2NO_String>
	<I2NO_IntegrationConsole_IEmployee_Array>return []</I2NO_IntegrationConsole_IEmployee_Array>
	<I2NO_System_Collections_Generic_IAsyncEnumerable_Of_IntegrationConsole_IEmployee_EndOf>return AsyncEnumerable.Empty_Of_IntegrationConsole.IEmployee_EndOf();</I2NO_System_Collections_Generic_IAsyncEnumerable_Of_IntegrationConsole_IEmployee_EndOf>
</PropertyGroup>

```

