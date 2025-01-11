using SalesDB.Models;

namespace SalesDB.DAL;

public interface ISalesRepository
{
    public IEnumerable<Sales> GetAllSales();
    public bool AddSale(Sale sale);
    public bool AddProduct(Product product);
}
