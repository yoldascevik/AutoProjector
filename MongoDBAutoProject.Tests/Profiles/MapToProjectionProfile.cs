using MongoDBAutoProject.Test.Models;

namespace MongoDBAutoProject.Test.Profiles;

public class MapToProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public MapToProjectionProfile()
    {
        EnableAutoMap = false;
        ForMember(x => x.Age).MapTo(x => x.PersonAge);
    }
}