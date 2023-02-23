using GraphQL.Api.Data.Entities;
using GraphQL.Api.Repository;
using GraphQL.DataLoader;
using GraphQL.Resolvers;
using GraphQL.Types;
using System.Security.Claims;

namespace GraphQL.Api.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepository,
                IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("The name of the product");
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            
            AddField(new FieldType
            {
                Name = "Type",
                Type= typeof(ProductTypeEnumType),
                Description = "The type of product"
            });

            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                resolve: context =>
                {
                    // for autorization we can use
                    var user = (ClaimsPrincipal)context.UserContext["user"];
                    
                    var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                            "GetReviewsByProductId", reviewRepository.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
