using GraphQL.Api.Data.Entities;
using GraphQL.Api.GraphQL.Types;
using GraphQL.Api.Repository;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL
{
    public class GraphQuery : ObjectGraphType
    {
        public GraphQuery(ProductRepository productRepository)
        {
            //Field<ListGraphType<ProductType>>("products")
            //    .Resolve(async context => await productRepository.GetAll());

            AddField(new FieldType
            {
                Name = "products",
                Type= typeof(ListGraphType<ProductType>),
                Resolver = new FuncFieldResolver<List<Product>>(async context => await productRepository.GetAll())
            });
        }
    }
}
