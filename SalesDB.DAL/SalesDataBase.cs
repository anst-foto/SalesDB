using Npgsql;
using SalesDB.Models;

namespace SalesDB.DAL;

public class SalesDataBase : ISalesRepository
{
    private readonly NpgsqlConnection _db;

    public SalesDataBase()
    {
        const string connectionString = "Server=127.0.0.1;Port=5432;Database=sales_db;User Id=postgres;Password=1234;SearchPath=test;";
        _db = new NpgsqlConnection(connectionString);
    }

    public IEnumerable<Sales> GetAllSales()
    {
        var sales = new List<Sales>();

        _db.Open();

        const string sql = "SELECT id, date, product_name, price, amount FROM view_sales";
        var command = new NpgsqlCommand(sql, _db);
        var result = command.ExecuteReader();
        while (result.Read())
        {
            sales.Add(new Sales
            {
                Id = result.GetInt64(0),
                Date = result.GetDateTime(1),
                ProductName = result.GetString(2),
                Price = result.GetDecimal(3),
                Amount = result.GetInt32(4)
            });
        }

        _db.Close();

        return sales;
    }

    public bool AddSale(Sale sale)
    {
        try
        {
            _db.Open();

            var sql = $"""
                           INSERT INTO table_sales(product_id, date, amount)
                           VALUES ({sale.ProductId}, '{sale.Date}', {sale.Amount})
                       """;
            var command = new NpgsqlCommand(sql, _db);
            var result = command.ExecuteNonQuery();

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

            var sql = $"""
                           INSERT INTO table_products(name, price)
                           VALUES ('{product.Name}', {product.Price})
                       """;
            var command = new NpgsqlCommand(sql, _db);
            var result = command.ExecuteNonQuery();

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

        var sql = $"CALL procedure_delete_product('{productName}')";
        var command = new NpgsqlCommand(sql, _db);
        command.ExecuteNonQuery();

        _db.Close();
    }
}
