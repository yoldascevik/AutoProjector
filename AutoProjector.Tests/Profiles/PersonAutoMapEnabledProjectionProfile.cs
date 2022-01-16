using AutoProjector.Tests.Models;

namespace AutoProjector.Tests.Profiles;

public class PersonAutoMapEnabledProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonAutoMapEnabledProjectionProfile()
    {
        EnableAutoMap = true;
    }
}