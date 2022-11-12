using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DebitManagement.Repository.Repositories;

public class DebitRepository : GenericRepository<Debit>, IDebitRepository
{
    private readonly DebitContext _context;

    public DebitRepository(DebitContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Debit>> GetUserDebits(string username)
    {
        return await _context.Debits.Include(x => x.User).Include(x => x.Product)
            .Where(x => x.User.Username == username && x.Status == "Active").ToListAsync();
    }

    public async Task<Debit?> GetEntityById(Guid id)
    {
        return await _context.Debits.Include(x => x.User).Include(x => x.Product)
            .Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}