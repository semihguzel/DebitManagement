namespace DebitManagement.Core.Entities;

public class Debit : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }
    public int Quantity { get; set; }

    public Product Product { get; set; }
    public User User { get; set; }
    public ICollection<DebitActionHistory> DebitActionHistories { get; set; }
}