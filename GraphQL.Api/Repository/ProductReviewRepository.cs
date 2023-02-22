
using GraphQL.Api.Data;
using GraphQL.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Api.Repository
{
    public class ProductReviewRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductReviewRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductReview> GetForProduct(int productId)
        {
            var r = _dbContext.ProductReviews.Where(pr => pr.ProductId == productId).ToList();
            return r;
        }

        public async Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> productIds)
        {
            var reviews = await _dbContext.ProductReviews.Where(pr => productIds.Contains(pr.ProductId)).ToListAsync();
            return reviews.ToLookup(r => r.ProductId);
        }
    }
}
