using DebitManagement.Data.Entities;
using DebitManagement.Data.Interfaces;

namespace DebitManagement.Repository.Repositories;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    private readonly DebitContext _context;

    public UserRoleRepository(DebitContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserRole?> GetRoleByName(string roleName)
    {
        return _context.UserRoles.FirstOrDefault(x => x.RoleName == roleName);
    }
}