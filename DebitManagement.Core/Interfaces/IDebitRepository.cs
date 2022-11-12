using DebitManagement.Core.Entities;

namespace DebitManagement.Core.Interfaces;

public interface IDebitRepository : IRepository<Debit>
{
    Task<IReadOnlyList<Debit>> GetUserDebits(string username);
    Task<Debit?> GetEntityById(Guid id);
}