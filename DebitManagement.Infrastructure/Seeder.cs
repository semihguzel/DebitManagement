using DebitManagement.Base.Helpers;
using DebitManagement.Core.Entities;

namespace DebitManagement.Repository;

public static class Seeder
{
    public static void Seed(this DebitContext debitContext)
    {
        var adminRole = new UserRole { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, RoleName = "Admin" };
        if (!debitContext.UserRoles.Any())
        {
            var userRole = new UserRole { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, RoleName = "User" };
            var managerRole = new UserRole { Id = Guid.NewGuid(), CreatedDate = DateTime.Now, RoleName = "Manager" };

            debitContext.UserRoles.AddRange(userRole, managerRole, adminRole);
            debitContext.SaveChanges();
        }

        if (!debitContext.Users.Any())
        {
            AuthHelper.CreatePasswordHash("admin", out byte[] passwordHash, out byte[] passwordSalt);
            var adminUser = new User
            {
                Id = Guid.NewGuid(), UserRoles = new List<UserRole> { adminRole }, CreatedDate = DateTime.Now,
                Username = "admin", PasswordHash = passwordHash, PasswordSalt = passwordSalt
            };

            debitContext.Users.Add(adminUser);
            debitContext.SaveChanges();
        }
    }
}