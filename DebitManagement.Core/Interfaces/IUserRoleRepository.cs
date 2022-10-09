using DebitManagement.Core.Entities;

namespace DebitManagement.Core.Interfaces;

public interface IUserRoleRepository : IRepository<UserRole>
{
    Task<UserRole?> GetRoleByName(string roleName);
}