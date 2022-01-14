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
        return Include(Name);
    }
        
    public ProjectionMember<TSource, TResult> Include(string memberName)
    {
        _projectionProfileContext.ProjectionDefinition = _projectionProfileContext.ProjectionDefinition.Include(new StringFieldDefinition<TSource>(memberName));
        _projectionProfileContext.RemoveExcludedMember(memberName);
        return this;
    }
        
    public ProjectionMember<TSource, TResult> Exclude()
    {
        _projectionProfileContext.ProjectionDefinition = _projectionProfileContext.ProjectionDefinition.Exclude(new StringFieldDefinition<TSource>(Name));
        _projectionProfileContext.AddExcludedMember(Name);
        return this;
    }
        
    public ProjectionMember<TSource, TResult> MapTo(Expression<Func<TResult, object>> member)
    {
        var memberExpression = (MemberExpression)member.Body;
        var aliasName = memberExpression.Member.Name;

        _projectionProfileContext.AddAlias(Name, aliasName);
            
        return this;
    }
}