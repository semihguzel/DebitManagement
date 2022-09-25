using DebitManagement.Data.Entities;

namespace DebitManagement.Data.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsername(string username);
}