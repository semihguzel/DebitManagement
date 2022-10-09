using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;

namespace DebitManagement.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly DebitContext _context;
    public ProductRepository(DebitContext context) : base(context)
    {
        _context = context;
    }
    
}