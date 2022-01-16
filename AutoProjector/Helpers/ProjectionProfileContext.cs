using MongoDB.Driver;

namespace AutoProjector.Helpers;

internal class ProjectionProfileContext<T>
{
    private readonly Dictionary<string, object> _aliases;

    public ProjectionProfileContext()
    {
        ExcludedMembers = new HashSet<string>();
        _aliases = new Dictionary<string, object>();
        ProjectionDefinition = Builders<T>.Projection.Combine();
    }
    
    public HashSet<string> ExcludedMembers { get; }
    public ProjectionDefinition<T> ProjectionDefinition { get; set; }
    public IReadOnlyDictionary<string, object> Aliases => _aliases;

    public void AddExcludedMember(string name) => ExcludedMembers.Add(name);
    public void RemoveExcludedMember(string name) => ExcludedMembers.Remove(name);
    public void AddAlias(string memberName, string aliasName) => _aliases.Add(aliasName, $"${memberName}");
}