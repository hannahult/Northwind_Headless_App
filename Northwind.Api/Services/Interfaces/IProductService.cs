using Northwind.Api.Entities;
using Northwind.Api.Enums;

namespace Northwind.Api.Services.Interfaces
{
    public interface IProductService
    {

        Task<List<Product>> GetAllProducts();
        Task<Product> GetProduct(int id);
        Task<ResponseCode> AddProduct(Product product);
        Task<ResponseCode> UpdateProduct(Product product);
        Task<ResponseCode> DeleteProduct(int id);




    }
}
