using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Api.GraphQL
{
    public class GraphSchema : Schema
    {
        public GraphSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<GraphQuery>();
        }
    }
}
