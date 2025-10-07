using Xunit;
using vendingmachine.Models;

namespace vendingmachine.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_ShouldDecreaseStock_WhenDecreaseStockCalled()
        {
            var product = new Product("Cola", 50, 5);

            product.DecreaseStock();

            Assert.Equal(4, product.Stock);
        }

        [Fact]
        public void Product_ShouldIncreaseStock_WhenAddStockCalled()
        {
            var product = new Product("Cola", 50, 2);

            product.AddStock(3);

            Assert.Equal(5, product.Stock);
        }

        [Fact]
        public void Product_ShouldReturnFalse_WhenStockIsZero()
        {
            var product = new Product("Cola", 50, 0);

            Assert.False(product.IsAvailable());
        }
    }
}

