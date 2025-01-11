using SalesDB.DAL;
using SalesDB.Models;

namespace SalesDB.BL;

public class SalesServices : IServices
{
    private readonly ISalesRepository _db;

    public SalesServices(ISalesRepository db)
    {
        _db = db;
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
