namespace SalesDB.Models;

public class Sales
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string ProductName { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
}
