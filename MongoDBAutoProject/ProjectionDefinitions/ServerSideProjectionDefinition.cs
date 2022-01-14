using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDBAutoProject.Extensions;

namespace MongoDBAutoProject.ProjectionDefinitions;

internal class ServerSideProjectionDefinition<TSource, TResult> : ProjectionDefinition<TSource, TResult>
{
    private readonly ProjectionProfile<TSource, TResult>? _profile;
    private readonly ProjectionDefinition<TSource> _projectionDefinition;

    public ServerSideProjectionDefinition()
    {
        _projectionDefinition = BuildProjectionDefinition();
    }

    public ServerSideProjectionDefinition(ProjectionProfile<TSource, TResult> profile)
    {
        _profile = profile ?? throw new ArgumentNullException(nameof(profile));
        _projectionDefinition = BuildProjectionDefinition(profile);
    }

    public override RenderedProjectionDefinition<TResult> Render(IBsonSerializer<TSource> sourceSerializer, IBsonSerializerRegistry serializerRegistry, LinqProvider linqProvider)
    {
        var bsonDocument = _projectionDefinition.Render(sourceSerializer, serializerRegistry);
            
        if (_profile != null && _profile.ProjectionProfileContext.Aliases.Any())
            bsonDocument.AddRange(_profile.ProjectionProfileContext.Aliases);

        return new RenderedProjectionDefinition<TResult>(bsonDocument, serializerRegistry.GetSerializer<TResult>());
    }

    private ProjectionDefinition<TSource> BuildProjectionDefinition(ProjectionProfile<TSource, TResult>? profile = null)
    {
        if (profile != null)
        {
            return profile.BuildProjectionDefinition();
        }

        return Builders<TSource>.Projection.CombineAutoMap<TSource, TResult>();
    }
}