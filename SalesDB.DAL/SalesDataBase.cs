using Dapper;
using Npgsql;
using SalesDB.Models;

namespace SalesDB.DAL;

public class SalesDataBase : ISalesRepository
{
    private readonly NpgsqlConnection _db;

    public SalesDataBase(string connectionString)
    {
        _db = new NpgsqlConnection(connectionString);
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public IEnumerable<Sales> GetAllSales()
    {
        _db.Open();

        const string sql = "SELECT id, date, product_name, price, amount FROM view_sales";
        var sales = _db.Query<Sales>(sql);

        _db.Close();

        return sales;
    }

    public bool AddSale(Sale sale)
    {
        try
        {
            _db.Open();

            const string sql = """
                                   INSERT INTO table_sales(product_id, date, amount)
                                   VALUES (@ProductId, @Date, @Amount)
                               """;
            var result = _db.Execute(sql, new { sale.ProductId, sale.Date, sale.Amount });

            return result > 0;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _db.Close();
        }
    }

    public bool AddProduct(Product product)
    {
        try
        {
            _db.Open();

            const string sql = """
                                   INSERT INTO table_products(name, price)
                                   VALUES (@Name, @Price)
                               """;
            var result = _db.Execute(sql, new { product.Name, product.Price });

            return result > 0;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _db.Close();
        }
    }

    public void DeleteProduct(string productName)
    {
        _db.Open();

        const string sql = "CALL procedure_delete_product(@ProductName)";
        _db.Execute(sql, new { productName });

        _db.Close();
    }

    public void DeleteSale(int id)
    {
        _db.Open();

        const string sql = "CALL procedure_delete_sale(@Id)";
        _db.Execute(sql, new { id });

        _db.Close();
    }
}
