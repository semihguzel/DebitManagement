namespace DebitManagement.Core.Entities;

public class DebitActionHistory : BaseEntity
{
    public Guid DebitId { get; set; }
    public string Action { get; set; }
    public string ActionDescription { get; set; }

    public Debit Debit { get; set; }
}