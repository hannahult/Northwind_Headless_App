using Microsoft.EntityFrameworkCore;
using Northwind.Api.Contexts;
using Northwind.Api.Entities;
using Northwind.Api.Enums;
using Northwind.Api.Services.Interfaces;


namespace Northwind.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _dbContext;

        public ProductService(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _dbContext.Products.Include(p => p.Category).ToListAsync();

            return products;
        }

        public async Task<Product?> GetProduct(int id)
        {
            var products = await _dbContext.Products
                 .Include(p => p.Category)
                 .FirstOrDefaultAsync(p => p.ProductId == id); ;

            return products;
        }

        //Felhantering behövs
        public async Task<ResponseCode> AddProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return ResponseCode.Created;    
        }
        public async Task<ResponseCode> UpdateProduct(Product product)
        {
            return ResponseCode.Accepted;
        }

        public async Task<ResponseCode> DeleteProduct(int id)
        {
         return ResponseCode.Accepted;
        }



    }
}
