using FluentAssertions;
using MongoDB.Driver;
using MongoDBAutoProject.Extensions;
using MongoDBAutoProject.Test.Helpers;
using MongoDBAutoProject.Test.Models;
using MongoDBAutoProject.Test.Profiles;
using Xunit;

namespace MongoDBAutoProject.Test;

public class AggregateFluentExtensionsTests
{
    private readonly IMongoCollection<Person> _personCollection;
    
    public AggregateFluentExtensionsTests()
    {
        var database = MongoDBHelper.GetMongoDatabase(true);
        _personCollection = MongoDBHelper.GetPersonCollection(database);
    }
    
    [Fact]
    public void ProjectTo_should_generate_the_correct_fields_when_a_projection_profile_is_used()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Aggregate()
            .Match(x=> x.Id == person.Id)
            .ProjectTo(new PersonProjectionProfile())
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            PersonAge = person.Age
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
    
    [Fact]
    public void ProjectTo_should_generate_the_correct_fields_when_a_projection_profile_is_not_used()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Aggregate()
            .Match(x=> x.Id == person.Id)
            .ProjectTo<Person, PersonDto>()
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
}