using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDBAutoProject.Helpers;
using MongoDBAutoProject.Extensions;

namespace MongoDBAutoProject;

public abstract class ProjectionProfile<TSource, TResult>
{
    private bool _enableAutoMap;
    private readonly List<ProjectionMember<TSource, TResult>> _projectionMembers;
    internal readonly ProjectionProfileContext<TSource> ProjectionProfileContext;

    protected ProjectionProfile()
    {
        _projectionMembers = new List<ProjectionMember<TSource, TResult>>();
        ProjectionProfileContext = new ProjectionProfileContext<TSource>();
    }

    /// <summary>
    /// Enable automatic mapping for all matching properties.
    /// </summary>
    protected bool EnableAutoMap
    {
        get => _enableAutoMap;
        set
        {
            if (value)
            {
                ForMember("_id").Include();
            }
            else
            {
                ForMember("_id").Exclude();
            }

            _enableAutoMap = value;
        }
    }

    protected ProjectionMember<TSource,TResult> ForMember<TProperty>(Expression<Func<TSource, TProperty>> member)
    {
        var memberExpression = (MemberExpression) member.Body;
        return ForMember(memberExpression.Member.Name);
    }

    protected ProjectionMember<TSource, TResult> ForMember(string memberName)
    {
        ProjectionMember<TSource, TResult>? member = _projectionMembers.FirstOrDefault(x => x.Name == memberName);
        if (member == null)
        {
            IReadOnlyCollection<string> sourceProperties = ProjectionPropertyCache<TSource>.PropertyNames;
            if (memberName != "_id" && !sourceProperties.Contains(memberName))
            {
                throw new ArgumentException($"Proptery \"{memberName}\" is not found in type {typeof(TSource).Name}");
            }
                
            member = new ProjectionMember<TSource, TResult>(ProjectionProfileContext, memberName);
            _projectionMembers.Add(member);
        }

        return member;
    }

    internal ProjectionDefinition<TSource> BuildProjectionDefinition()
    {
        var builder = Builders<TSource>.Projection;
        var projectionDefinition = ProjectionProfileContext.ProjectionDefinition;
            
        if (EnableAutoMap)
        {
            var excludedMemberNames = ProjectionProfileContext.ExcludedMembers.ToArray();
            var automapMembers = builder.CombineAutoMap<TSource, TResult>(excludedMemberNames);
                
            return builder.Combine(projectionDefinition, automapMembers);
        }
        
        return projectionDefinition;
    }
}