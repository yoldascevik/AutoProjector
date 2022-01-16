using AutoProjector.Tests.Models;

namespace AutoProjector.Tests.Profiles;

public class PersonProjectionTestProfile : ProjectionProfile<Person, PersonDto>
{
    public PersonProjectionTestProfile(bool useExpressionParameter)
    {
        EnableAutoMap = false;

        if (useExpressionParameter)
        {
            ForMember(x => x.FirstName).Include();
        }
        else
        {
            ForMember(nameof(Person.FirstName)).Include();
        }
    }
}