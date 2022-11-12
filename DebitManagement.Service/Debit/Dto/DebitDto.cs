namespace DebitManagement.Service.Debit.Dto;

public class DebitDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public string Username { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }
}