namespace DebitManagement.Data.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}