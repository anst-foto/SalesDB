using Npgsql;
using SalesDB.Models;

namespace SalesDB.DAL;

public class SalesDataBase : ISalesRepository
{
    private readonly NpgsqlConnection _db;

    public SalesDataBase(string connectionString)
    {
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

            const string sql = """
                                    INSERT INTO table_sales(product_id, date, amount)
                                    VALUES (@ProductId, @Date, @Amount)
                                """;
            var command = new NpgsqlCommand(sql, _db);
            command.Parameters.AddWithValue("@ProductId", sale.ProductId);
            command.Parameters.AddWithValue("@Date", sale.Date);
            command.Parameters.AddWithValue("@Amount", sale.Amount);
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

            const string sql = """
                                    INSERT INTO table_products(name, price)
                                    VALUES (@Name, @Price)
                                """;
            var command = new NpgsqlCommand(sql, _db);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
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

        const string sql = "CALL procedure_delete_product(@ProductName)";
        var command = new NpgsqlCommand(sql, _db);
        command.Parameters.AddWithValue("@ProductName", productName);
        command.ExecuteNonQuery();

        _db.Close();
    }

    public void DeleteSale(int id)
    {
        _db.Open();

        const string sql = "CALL procedure_delete_sale(@Id)";
        var command = new NpgsqlCommand(sql, _db);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();

        _db.Close();
    }
}
