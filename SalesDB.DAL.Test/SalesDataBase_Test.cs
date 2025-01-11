using SalesDB.Models;

namespace SalesDB.DAL.Test
{
    public class SalesDataBase_Test
    {
        [Fact]
        public void GetAllSales_Test()
        {
            var expected = new List<Sales>()
            {
                new()
                {
                    Id = 1,
                    Date = new DateTime(2020, 1, 1),
                    ProductName = "product_1",
                    Price = 10,
                    Amount = 10
                },
                new()
                {
                    Id = 2,
                    Date = new DateTime(2020, 1, 2),
                    ProductName = "product_2",
                    Price = 20,
                    Amount = 2
                },
                new()
                {
                    Id = 3,
                    Date = new DateTime(2020, 1, 3),
                    ProductName = "product_3",
                    Price = 30,
                    Amount = 30
                }
            };

            var db = new SalesDataBase();
            var actual = db.GetAllSales();

            Assert.Equal(expected, actual);
        }
    }
}
