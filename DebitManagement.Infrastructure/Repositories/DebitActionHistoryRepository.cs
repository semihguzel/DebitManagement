using DebitManagement.Core.Interfaces;
using DebitActionHistory = DebitManagement.Core.Entities.DebitActionHistory;

namespace DebitManagement.Repository.Repositories;

public class DebitActionHistoryRepository : GenericRepository<DebitActionHistory>, IDebitActionHistoryRepository
{
    private readonly DebitContext _context;

    public DebitActionHistoryRepository(DebitContext context) : base(context)
    {
        _context = context;
    }
}