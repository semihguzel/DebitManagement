using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebitManagement.Repository.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    private readonly DebitContext _context;

    public ProductTypeRepository(DebitContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ProductType?> GetByCode(string roleTypeCode)
    {
        return await _context.ProductTypes.FirstOrDefaultAsync(x => x.ProductTypeCode == roleTypeCode);
    }
}