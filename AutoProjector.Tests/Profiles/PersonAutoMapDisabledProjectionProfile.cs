using AutoProjector.Tests.Models;

namespace AutoProjector.Tests.Profiles;

public class PersonAutoMapDisabledProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonAutoMapDisabledProjectionProfile()
    {
        EnableAutoMap = false;
        ForMember(x => x.FirstName).MapTo(x => x.FirstName);
    }
}