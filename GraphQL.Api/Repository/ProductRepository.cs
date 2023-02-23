using GraphQL.Api.Data;
using GraphQL.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Api.Repository
{
    public class ProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context=context;
        }

        public Task<List<Product>> GetAll()
        {
            return context.Products.ToListAsync();
        }
        public Task<Product> GetOne(int id)
        {
            return context.Products.SingleAsync(p => p.Id == id);
        }
    }
}
