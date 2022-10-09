using DebitManagement.Core.Entities;

namespace DebitManagement.Core.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByCode(string code);
}