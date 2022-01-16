using MongoDB.Driver;

namespace AutoProjector.Extensions;

public static class FindFluentExtensions
{
    public static IFindFluent<TSource, TResult> ProjectTo<TSource, TResult>(this IFindFluent<TSource, TSource> findFluent)
    {
        var serverSideProjectionDefinition = Builders<TSource>.Projection.ServerSide<TSource, TResult>();
        return findFluent.Project(serverSideProjectionDefinition);
    }

    public static IFindFluent<TSource, TResult> ProjectTo<TSource, TResult>(this IFindFluent<TSource, TSource> findFluent, ProjectionProfile<TSource, TResult> profile)
    {
        var serverSideProjectionDefinition = Builders<TSource>.Projection.ServerSide(profile);
        return findFluent.Project(serverSideProjectionDefinition);
    }
}