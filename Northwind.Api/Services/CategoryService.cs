using Microsoft.EntityFrameworkCore;
using Northwind.Api.Contexts;
using Northwind.Api.DTOs;
using Northwind.Api.Enums;
using Northwind.Api.Services.Interfaces;
namespace Northwind.Api.Services

{
    public class CategoryService : ICategoryService
    {
        private readonly NorthwindContext _dbContext;

        public CategoryService(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _dbContext.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }
    }
}
