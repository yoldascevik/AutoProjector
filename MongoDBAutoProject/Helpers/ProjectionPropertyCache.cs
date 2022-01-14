using System.Collections.ObjectModel;
using System.Reflection;

namespace MongoDBAutoProject.Helpers;

internal class ProjectionPropertyCache<T>
{
    static ProjectionPropertyCache()
    {
        PropertyNames = GetProperties();
    }

    public static IReadOnlyCollection<string> PropertyNames { get; }
        
    private static ReadOnlyCollection<string> GetProperties()
    {
        return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => !prop.IsSpecialName)
            .Select(prop => prop.Name).ToList()
            .AsReadOnly();
    }
}