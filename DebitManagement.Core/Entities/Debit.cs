namespace DebitManagement.Data.Entities;

public class Debit : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }
    public string ActionDescription { get; set; }

    public Product Product { get; set; }
    public User User { get; set; }
}