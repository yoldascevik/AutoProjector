using AutoProjector.Tests.Models;

namespace AutoProjector.Tests.Profiles;

public class MapToProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public MapToProjectionProfile()
    {
        EnableAutoMap = false;
        ForMember(x => x.Age).MapTo(x => x.PersonAge);
    }
}