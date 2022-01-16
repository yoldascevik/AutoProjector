using AutoProjector.Helpers;
using AutoProjector.ProjectionDefinitions;
using MongoDB.Driver;

namespace AutoProjector.Extensions;

internal static class ProjectionDefinitionBuilderExtensions
{
    public static ServerSideProjectionDefinition<TSource, TResult> ServerSide<TSource, TResult>(this ProjectionDefinitionBuilder<TSource> builder)
    {
        return new ServerSideProjectionDefinition<TSource, TResult>();
    }

    public static ServerSideProjectionDefinition<TSource, TResult> ServerSide<TSource, TResult>(this ProjectionDefinitionBuilder<TSource> builder, ProjectionProfile<TSource, TResult> profile)
    {
        return new ServerSideProjectionDefinition<TSource, TResult>(profile);
    }

    public static ProjectionDefinition<TSource> CombineAutoMap<TSource, TResult>(this ProjectionDefinitionBuilder<TSource> builder, params string[]? excludeMemberNames)
    {
        var sourceProperties = ProjectionPropertyCache<TSource>.PropertyNames;
        var resultProperties = ProjectionPropertyCache<TResult>.PropertyNames;

        var projectionDefinitions = resultProperties.Intersect(sourceProperties)
            .Where(name => excludeMemberNames == null || !excludeMemberNames.Contains(name))
            .Select(name => builder.Include(new StringFieldDefinition<TSource>(name)))
            .ToList();

        return builder.Combine(projectionDefinitions);
    }
}