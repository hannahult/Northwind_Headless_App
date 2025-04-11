using Microsoft.EntityFrameworkCore;
using Northwind.Api.Contexts;
using Northwind.Api.DTOs;
using Northwind.Api.Services.Interfaces;

namespace Northwind.Api.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly NorthwindContext _dbContext;

        public SupplierService(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SupplierDTO>> GetAllSuppliersAsync()
        {
            return await _dbContext.Suppliers
                .Select(s => new SupplierDTO
                {
                    SupplierId = s.SupplierId,
                    CompanyName = s.CompanyName
                })
                .ToListAsync();
        }
    }
}
