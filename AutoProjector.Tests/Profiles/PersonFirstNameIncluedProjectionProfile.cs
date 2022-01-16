using AutoProjector.Tests.Models;

namespace AutoProjector.Tests.Profiles;

public class PersonFirstNameIncluedProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonFirstNameIncluedProjectionProfile()
    {
        EnableAutoMap = false;
        ForMember(x => x.FirstName).Include();
    }
}