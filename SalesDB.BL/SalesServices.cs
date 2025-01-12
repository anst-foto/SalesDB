using SalesDB.DAL;
using SalesDB.Models;

namespace SalesDB.BL;

public class SalesServices : IServices
{
    private readonly ISalesRepository _db;

    public SalesServices(string connectionString)
    {
        _db = new SalesDataBase(connectionString);
    }

    public IEnumerable<Sales> GetAllSales()
    {
        return _db.GetAllSales();
    }

    public bool AddSale(Sale sale)
    {
        return _db.AddSale(sale);
    }

    public bool AddProduct(Product product)
    {
        return _db.AddProduct(product);
    }
}
