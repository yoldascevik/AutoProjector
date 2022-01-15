using FluentAssertions;
using MongoDB.Driver;
using MongoDBAutoProject.Extensions;
using MongoDBAutoProject.Test.Helpers;
using MongoDBAutoProject.Test.Models;
using MongoDBAutoProject.Test.Profiles;
using Xunit;

namespace MongoDBAutoProject.Test;

public class ProjectionMemberTests
{
    private readonly IMongoCollection<Person> _personCollection;
    
    public ProjectionMemberTests()
    {
        var database = MongoDBHelper.GetMongoDatabase(true);
        _personCollection = MongoDBHelper.GetPersonCollection(database);
    }
    
    [Fact]
    public void Include_should_generate_only_the_included_fields()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Find(x=> x.Id == person.Id)
            .ProjectTo(new PersonFirstNameIncluedProjectionProfile())
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            FirstName = person.FirstName
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
    
    [Fact]
    public void Exclude_should_generate_the_all_name_matching_fields_except_exluded_fields()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Find(x=> x.Id == person.Id)
            .ProjectTo(new PersonFirstNameExcludedProjectionProfile())
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            Id = person.Id,
            FirstName = null,
            LastName = person.LastName,
            PersonAge = person.Age
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
    
    [Fact]
    public void MapTo_should_generate_the_mapped_field_as_alias_name()
    {
        Person person = _personCollection.AsQueryable().First();
        PersonDto? actualProjection = _personCollection.Find(x=> x.Id == person.Id)
            .ProjectTo(new MapToProjectionProfile())
            .FirstOrDefault();

        var expectedProjection = new PersonDto()
        {
            PersonAge = person.Age
        };

        actualProjection.Should().BeEquivalentTo(expectedProjection);
    }
}