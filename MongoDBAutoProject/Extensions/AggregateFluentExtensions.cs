using MongoDB.Driver;

namespace MongoDBAutoProject.Extensions;

public static class AggregateFluentExtensions
{
    public static IAggregateFluent<TResult> ProjectTo<TSource, TResult>(this IAggregateFluent<TSource> aggregateFluent)
    {
        var serverSideProjectionDefinition = Builders<TSource>.Projection.ServerSide<TSource, TResult>();
        return aggregateFluent.Project(serverSideProjectionDefinition);
    }

    public static IAggregateFluent<TResult> ProjectTo<TSource, TResult>(this IAggregateFluent<TSource> aggregateFluent, ProjectionProfile<TSource, TResult> profile)
    {
        var serverSideProjectionDefinition = Builders<TSource>.Projection.ServerSide(profile);
        return aggregateFluent.Project(serverSideProjectionDefinition);
    }
}