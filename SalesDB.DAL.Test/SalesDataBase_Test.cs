using Microsoft.Extensions.Configuration;
using SalesDB.Models;

namespace SalesDB.DAL.Test;

    public class SalesDataBase_Test
    {
        private readonly List<Sales> _salesList =
        [
            new Sales
            {
                Id = 1,
                Date = new DateTime(2020, 1, 1),
                ProductName = "product_1",
                Price = 10,
                Amount = 10
            },

            new Sales
            {
                Id = 2,
                Date = new DateTime(2020, 1, 2),
                ProductName = "product_2",
                Price = 20,
                Amount = 20
            },

            new Sales
            {
                Id = 3,
                Date = new DateTime(2020, 1, 3),
                ProductName = "product_3",
                Price = 30,
                Amount = 30
            }
        ];

        private readonly SalesDataBase _db;

        public SalesDataBase_Test()
        {
            var connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .GetConnectionString("ConnectionToTestDB");

            _db = new SalesDataBase(connectionString);
        }

        [Fact]
        public void GetAllSales_Test()
        {
            var actual = _db.GetAllSales().ToList();

            /*Assert.Multiple(
                () => Assert.Equal(expected.Count, actual.Count),
                () => Assert.Equal(expected[0], actual[0]),
                () => Assert.Equal(expected[1], actual[1]),
                () => Assert.Equal(expected[2], actual[2]));*/
            Assert.Equal(_salesList, actual);
        }

        [Fact]
        public void AddProduct_NegativeTest()
        {
            var result = _db.AddProduct(new Product()
            {
                Name = "product_1",
                Price = 10
            });

            Assert.False(result);
        }

        [Fact]
        public void AddProduct_PositiveTest()
        {
            const string productName = "product";

            var result = _db.AddProduct(new Product()
            {
                Name = productName,
                Price = 1
            });
            _db.DeleteProduct(productName);

            Assert.True(result);
        }

        [Fact]
        public void AddSale_NegativeTest()
        {
            var result = _db.AddSale(
                new Sale()
            {
        ProductId = 1,
        Amount = -1,
        Date = DateTime.Now
            });

            Assert.False(result);
        }

        [Fact]
        public void AddSale_PositiveTest()
        {
            const int id = 1;

            var result = _db.AddSale(
                new Sale()
                {
                    ProductId = id,
                    Amount = 1,
                    Date = DateTime.Now
                });
            _db.DeleteSale(id);

            Assert.True(result);
        }
    }

