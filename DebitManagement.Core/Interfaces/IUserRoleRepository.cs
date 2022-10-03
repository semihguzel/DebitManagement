using DebitManagement.Data.Entities;

namespace DebitManagement.Data.Interfaces;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<UserRole?> GetRoleByName(string roleName);
}