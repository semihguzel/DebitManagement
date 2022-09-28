namespace DebitManagement.Data.Entities;

public class UserRole : BaseEntity
{
    public string RoleName { get; set; }

    public ICollection<User> Users { get; set; }
}