using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebitManagement.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly DebitContext _context;

    public ProductRepository(DebitContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Product?> GetByCode(string code)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.ProductCode == code);
    }
}