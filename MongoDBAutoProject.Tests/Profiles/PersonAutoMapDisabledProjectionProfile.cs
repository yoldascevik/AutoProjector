using MongoDBAutoProject.Test.Models;

namespace MongoDBAutoProject.Test.Profiles;

public class PersonAutoMapDisabledProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonAutoMapDisabledProjectionProfile()
    {
        EnableAutoMap = false;
        ForMember(x => x.FirstName).MapTo(x => x.FirstName);
    }
}