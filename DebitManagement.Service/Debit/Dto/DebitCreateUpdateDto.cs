namespace DebitManagement.Service.Debit.Dto;

public class DebitCreateUpdateDto
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ReturnDate { get; set; }
    public int Quantity { get; set; }
}