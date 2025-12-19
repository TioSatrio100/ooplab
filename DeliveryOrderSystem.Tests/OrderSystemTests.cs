using Xunit;
using System.Collections.Generic;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Builders;
using DeliveryOrderSystem.Strategies;
using DeliveryOrderSystem.Decorators;
using DeliveryOrderSystem.Factories;
using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.States;

namespace DeliveryOrderSystem.Tests
{
    public class OrderSystemTests
    {
        // Setup dasar untuk Builder dan Director
        private readonly StandardOrderBuilder _builder = new StandardOrderBuilder();
        private readonly OrderDirector _director;

        public OrderSystemTests()
        {
            _director = new OrderDirector(_builder);
        }

        // Test builder and strategy
        [Fact]
        public void Builder_ShouldCreateOrderWithCorrectStrategiesAndCost()
        {
            // Buat pesanan standar yang mencakup diskon Loyalitas dan pengiriman Standar (gratis jika > 100)
            Order order = _director.BuildStandardLoyaltyOrder(); // Total item: 65 + (8*2) = 81.00

            // Pengujian Item dan Biaya
            Assert.Equal(2, order.ItemsAsOrderItems.Count); 
            Assert.Equal(87.90m, order.CalculateTotalCost());
            
            // Pengujian Strategi
            Assert.Contains(order.CostStrategies, s => s is LoyaltyDiscount);
            Assert.Contains(order.CostStrategies, s => s is StandardDeliveryFee);
        }

        // Test state pattern
        [Fact]
        public void State_ShouldTransitionCorrectly()
        {
            // Buat pesanan dasar
            Order order = new Order(); 

            // ASSERT Awal
            Assert.IsType<PreparationState>(order.CurrentState);

            // ACT 1: Pindah ke tahap Delivery
            order.ProceedToNextState(); 
            Assert.IsType<DeliveryState>(order.CurrentState);

            // ACT 2: Pindah ke tahap Completed
            order.ProceedToNextState(); 
            Assert.IsType<CompletedState>(order.CurrentState);

            // ACT 3: Coba pindah lagi (Completed seharusnya tetap Completed)
            order.ProceedToNextState(); 
            Assert.IsType<CompletedState>(order.CurrentState);
        }

        [Fact]
        public void State_ShouldHandleCancellation()
        {
            Order order = new Order(); // Status awal: PreparationState

            // ACT 1: Batalkan saat Preparation
            order.Cancel();
            Assert.IsType<CancelledState>(order.CurrentState);

            // ACT 2: Coba batalkan lagi (harusnya tetap Cancelled)
            order.Cancel();
            Assert.IsType<CancelledState>(order.CurrentState);

            // ARRANGE 2: Buat pesanan baru dan majukan ke Delivery
            Order deliveryOrder = new Order();
            deliveryOrder.ProceedToNextState(); 
        }

        // Test decoration pattern
        [Fact]
        public void Decorator_ShouldModifyDescriptionAndPrice()
        {
            // Item Dasar
            MenuItem basicBurger = new MenuItem { Name = "Basic Burger", Price = 50.00m, Quantity = 1 }; // Harga 50.00

            // Dekorasi dengan Extra Sauce (+5.00)
            IOrderItem itemWithSauce = new ExtraSauceDecorator(basicBurger); 
            Assert.Equal(55.00m, itemWithSauce.GetPrice());
        }

        // Test abstract
        [Fact]
        public void AbstractFactory_ShouldCreateFamilyOfProducts()
        {
            // Gunakan Gourmet Factory
            IMenuFactory gourmetFactory = new GourmetMenuFactory();
            MenuItem gourmetMain = gourmetFactory.CreateMainDish();
            MenuItem gourmetSide = gourmetFactory.CreateSideDish();

            // Memastikan nama dan harga sesuai tema Gourmet
            Assert.Contains("Wagyu Steak", gourmetMain.Name);
            Assert.True(gourmetMain.Price > 200.00m);
            Assert.Contains("Truffle", gourmetSide.Name);
            
            // Gunakan Budget Factory
            IMenuFactory budgetFactory = new BudgetMenuFactory();
            MenuItem budgetMain = budgetFactory.CreateMainDish();
            MenuItem budgetSide = budgetFactory.CreateSideDish();

            // Memastikan nama dan harga sesuai tema Budget
            Assert.Contains("Ayam Geprek", budgetMain.Name);
            Assert.True(budgetMain.Price < 30.00m);
            Assert.Contains("Tahu Goreng", budgetSide.Name);
        }
    }
}
