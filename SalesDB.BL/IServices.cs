using SalesDB.Models;

namespace SalesDB.BL;

public interface IServices
{
    public IEnumerable<Sales> GetAllSales();
    public bool AddSale(Sale sale);
    public bool AddProduct(Product product);
}
