using GraphQL.Types;
using System;

namespace Dashboard.GraphQL
{
    public class GraphQLSchema : Schema, ISchema
    {
        public GraphQLSchema(IServiceProvider resolver) : base(resolver)
        {
            Query = (DashboardQuery)resolver.GetService(typeof(DashboardQuery));
            Mutation = (AccountMutation)resolver.GetService(typeof(AccountMutation));
        }
    }
}
