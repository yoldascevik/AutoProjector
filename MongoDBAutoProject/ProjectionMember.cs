using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDBAutoProject.Helpers;

namespace MongoDBAutoProject;

public class ProjectionMember<TSource, TResult>
{
    private readonly ProjectionProfileContext<TSource> _projectionProfileContext;
        
    internal ProjectionMember(ProjectionProfileContext<TSource> projectionProfileContext, string name)
    {       
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _projectionProfileContext = projectionProfileContext ?? throw new ArgumentNullException(nameof(projectionProfileContext));
    }
	
    public string Name { get;}

    public ProjectionMember<TSource, TResult> Include()
    {
        _projectionProfileContext.ProjectionDefinition = _projectionProfileContext.ProjectionDefinition.Include(new StringFieldDefinition<TSource>(Name));
        _projectionProfileContext.RemoveExcludedMember(Name);
        return this;
    }
        
    public ProjectionMember<TSource, TResult> Exclude()
    {
        if (Name.Equals("_id", StringComparison.CurrentCultureIgnoreCase) 
            || Name.Equals("id", StringComparison.CurrentCultureIgnoreCase) )
        {
            _projectionProfileContext.ProjectionDefinition = _projectionProfileContext.ProjectionDefinition.Exclude(new StringFieldDefinition<TSource>(Name));
        }
        
        _projectionProfileContext.AddExcludedMember(Name);
        return this;
    }
        
    public ProjectionMember<TSource, TResult> MapTo<TProperty>(Expression<Func<TResult, TProperty>> member)
    {
        var memberExpression = (MemberExpression)member.Body;
        var aliasName = memberExpression.Member.Name;

        _projectionProfileContext.AddAlias(Name, aliasName);
        
        return this;
    }
}