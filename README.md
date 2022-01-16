## AutoProjector

AutoProjector is mapping library for MongoDB Driver. This library allows you to simplify the mongodb projection through profiles.

### Installation

You can include this library in your project with the Nuget package.

Add the nuget package to your project using *Package Manager Console* or *Nuget Package Manager*.

```powershell
PM> Install-Package AutoProjector
```
Or using .Net Cli

```powershell
> dotnet add package AutoProjector
```

### Sample Use
Profile class
```csharp
public class PersonProjectionProfile : ProjectionProfile<Person, PersonDto>  
{  
    public PersonProjectionProfile()  
    {  
        EnableAutoMap = true;  
        ForMember(x => x.LastName).Exclude();  
        ForMember(x => x.Age).MapTo(x => x.PersonAge);  
    }  
}
```
ProjectTo Extension Using
```csharp
public PersonDto GetPersonDtoById(int personId)  
{  
    PersonDto personDto = _personCollection.Find(x=> x.Id == personId)  
        .ProjectTo(new PersonProjectionProfile())  
        .FirstOrDefault();  
  
    return personDto;  
}
```

###  Profile Operations

|Name  | Description  |
|--|--|
|Include | Include property to projection |
|Exclude | Exclude property to projection |
|MapTo	 | Map property to property with a different name |
|EnableAutoMap | If true, it automatically adds all the properties whose name matches, otherwise it is necessary to manually include the fields to be added.

## Contributing

Please browse the [CONTRIBUTING.md](https://github.com/yoldascevik/AutoProjector/blob/master/CONTRIBUTING.md) file to contribute.
