using MongoDBAutoProject.Test.Models;

namespace MongoDBAutoProject.Test.Profiles;

public class PersonFirstNameIncluedProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonFirstNameIncluedProjectionProfile()
    {
        EnableAutoMap = false;
        ForMember(x => x.FirstName).Include();
    }
}