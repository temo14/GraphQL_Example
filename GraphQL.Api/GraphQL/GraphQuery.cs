using GraphQL.Api.Data.Entities;
using GraphQL.Api.GraphQL.Types;
using GraphQL.Api.Repository;
using GraphQL.Resolvers;
using GraphQL.Types;

namespace GraphQL.Api.GraphQL;

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
        AddField(new FieldType
        {
            Name = "product",
            Type= typeof(ProductType),
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
            Resolver = new FuncFieldResolver<Product>(async context =>
            {
                try
                {
                    var result = await productRepository.GetOne(context.GetArgument<int>("id"));
                    return result;
                }
                catch (Exception ex)
                {
                    // log or handle the exception here
                    throw ex;
                }
            })
        });
    }
}