using DebitManagement.Core.Entities;

namespace DebitManagement.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsername(string username);
}