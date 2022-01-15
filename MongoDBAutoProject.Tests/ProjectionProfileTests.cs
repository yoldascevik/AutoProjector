using FluentAssertions;
using MongoDB.Driver;
using MongoDBAutoProject.Extensions;
using MongoDBAutoProject.Test.Helpers;
using MongoDBAutoProject.Test.Models;
using MongoDBAutoProject.Test.Profiles;
using Xunit;

namespace MongoDBAutoProject.Test;

public class ProjectionProfileTests
{
    private readonly IMongoCollection<Person> _personCollection;
    
    public ProjectionProfileTests()
    {
        var database = MongoDBHelper.GetMongoDatabase(true);
        _personCollection = MongoDBHelper.GetPersonCollection(database);
    }
    
    [Fact]
    public void EnableAutoMap_should_generate_the_all_matching_fields_when_enabled()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Find(x=> x.Id == person.Id)
            .ProjectTo(new PersonAutoMapEnabledProjectionProfile())
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
    
    [Fact]
    public void EnableAutoMap_should_generate_only_included_fields_when_disabled()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Find(x=> x.Id == person.Id)
            .ProjectTo(new PersonAutoMapDisabledProjectionProfile())
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            FirstName = person.FirstName
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ForMember_should_generate_the_correct_fields(bool useExpressionParameter)
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Find(x=> x.Id == person.Id)
            .ProjectTo(new PersonProjectionTestProfile(useExpressionParameter))
            .FirstOrDefault();
        
        var expectedProjection = new PersonDto()
        {
            FirstName = person.FirstName,
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
}