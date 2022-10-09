namespace DebitManagement.Service.Product.Dto;

public class ProductDto
{
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public double Price { get; set; }

    public Guid ProductTypeId { get; set; }
}