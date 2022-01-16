using System.Collections.Generic;
using Bogus;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Person = AutoProjector.Tests.Models.Person;

namespace AutoProjector.Tests.Helpers;

internal class MongoDBHelper
{
    public static IMongoDatabase GetMongoDatabase(bool insertMockData)
    {
        var configuration = ConfigurationHelper.InitConfiguration();
        var mongoUrl = MongoUrl.Create(configuration.GetConnectionString("Mongo"));
        var mongoClient = new MongoClient(mongoUrl);
        var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

        if (insertMockData)
        {
            Initialize(database);
        }

        return database;
    }

    public static IMongoCollection<Person> GetPersonCollection(IMongoDatabase database)
    {
        return database.GetCollection<Person>("persons");
    }

    public static void Initialize(IMongoDatabase database)
    {
        IMongoCollection<Person> personCollection = database.GetCollection<Person>("persons");

        if (!personCollection.AsQueryable().Any())
        {
            int personId = 1;
            List<Person> personList = new Faker<Person>()
                .RuleFor(x => x.Id, _ => personId++)
                .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                .RuleFor(x => x.LastName, f => f.Person.LastName)
                .RuleFor(x => x.Age, f => f.Random.Number(15, 70))
                .Generate(10);

            personCollection.InsertMany(personList);
        }
    }
}