using DebitManagement.Data.Entities;
using DebitManagement.Data.Interfaces;
using DebitManagement.Repository.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DebitManagement.Repository.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly DebitContext _context;

    public UserRepository(DebitContext context) : base(context)
    {
        _context = context;
    }


    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Users.Include(x => x.UserRoles)
            .FirstOrDefaultAsync(x => x.Username == username);
    }
}