namespace SalesDB.Models;

public class Sale
{
    public long Id { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public DateTime Date { get; set; }
}
