using Northwind.Api.DTOs;

namespace Northwind.Api.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync();

    }
}
