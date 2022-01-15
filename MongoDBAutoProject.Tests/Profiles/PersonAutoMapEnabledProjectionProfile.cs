using MongoDBAutoProject.Test.Models;

namespace MongoDBAutoProject.Test.Profiles;

public class PersonAutoMapEnabledProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonAutoMapEnabledProjectionProfile()
    {
        EnableAutoMap = true;
    }
}