namespace DebitManagement.Core.Entities;

public class ProductType : BaseEntity
{
    public string ProductTypeCode { get; set; }
    public string ProductTypeDescription { get; set; }

    public ICollection<Product> Products { get; set; }
}