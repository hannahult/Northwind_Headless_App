using Northwind.Api.DTOs;

namespace Northwind.Api.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<List<SupplierDTO>> GetAllSuppliersAsync();
    }
}
