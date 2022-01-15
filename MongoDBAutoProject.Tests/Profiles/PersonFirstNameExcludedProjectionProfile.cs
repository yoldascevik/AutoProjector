﻿using MongoDBAutoProject.Test.Models;

namespace MongoDBAutoProject.Test.Profiles;

public class PersonFirstNameExcludedProjectionProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonFirstNameExcludedProjectionProfile()
    {
        EnableAutoMap = true;
        ForMember(x => x.FirstName).Exclude();
        ForMember(x => x.Age).MapTo(x => x.PersonAge);
    }
}