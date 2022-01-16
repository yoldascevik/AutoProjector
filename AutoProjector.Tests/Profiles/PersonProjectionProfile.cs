using AutoProjector.Tests.Models;

namespace AutoProjector.Tests.Profiles;

public class PersonProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonProjectionProfile()
    {
        EnableAutoMap = true;
        ForMember(x => x.Age).MapTo(x => x.PersonAge);
    }
}