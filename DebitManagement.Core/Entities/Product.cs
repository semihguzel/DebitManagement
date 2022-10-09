namespace DebitManagement.Core.Entities;

public class Product : BaseEntity
{
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public double Price { get; set; }
    public Guid ProductTypeId { get; set; }

    public ProductType ProductType { get; set; }
    public ICollection<Debit> Debits { get; set; }
}