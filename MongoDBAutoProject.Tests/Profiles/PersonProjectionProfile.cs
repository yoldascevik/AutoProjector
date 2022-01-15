using MongoDBAutoProject.Test.Models;

namespace MongoDBAutoProject.Test.Profiles;

public class PersonProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonProjectionProfile()
    {
        EnableAutoMap = true;
        ForMember(x => x.Age).MapTo(x => x.PersonAge);
    }
}