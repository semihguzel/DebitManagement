using DebitManagement.Core.Entities;

namespace DebitManagement.Core.Interfaces;

public interface IProductTypeRepository : IRepository<ProductType>
{
    Task<ProductType?> GetByCode(string roleTypeCode);
}