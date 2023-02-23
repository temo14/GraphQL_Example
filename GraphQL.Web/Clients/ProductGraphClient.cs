using GraphQL.Client.Http;
using GraphQL.Web.Models;
using Newtonsoft.Json;

namespace GraphQL.Web.Clients
{
    public class ProductGraphClient
    {
        private readonly GraphQLHttpClient _client;

        public ProductGraphClient(GraphQLHttpClient client)
        {
            _client = client;
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            var query = new GraphQLRequest
            {
                Query = @"query productQuery($productId: ID!)
        { 
            product(id: $productId) 
            { 
                id 
                name 
                price 
                rating 
                photoFileName 
                description 
                stock 
                introducedAt 
                reviews { title review }
            }
        }",
                Variables = new { productId = id }
            };

            var response = await _client.SendQueryAsync<dynamic>(query);
            var json = response.Data.product.ToString();
            var product = JsonConvert.DeserializeObject<ProductModel>(json);
            
            return product;
        }


        //public async Task<ProductReviewModel> AddReview(ProductReviewInputModel review)
        //{
        //    var query = new GraphQLRequest
        //    {
        //        Query = @" 
        //        mutation($review: reviewInput!)
        //        {
        //            createReview(review: $review)
        //            {
        //                id
        //            }
        //        }",
        //        Variables = new { review }
        //    };
        //    var response = await _client.PostAsync(query);
        //    return response.GetDataFieldAs<ProductReviewModel>("createReview");
        //}
    }
}
