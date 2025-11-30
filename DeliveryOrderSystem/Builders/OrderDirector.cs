using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Strategies;
using System.Collections.Generic;

namespace DeliveryOrderSystem.Builders
{
    public class OrderDirector
    {
        private readonly IOrderBuilder _builder;

        public OrderDirector(IOrderBuilder builder)
        {
            _builder = builder;
        }

        // Metode untuk membuat pesanan standar (dengan diskon loyalitas)
        public Order BuildStandardLoyaltyOrder()
        {
            _builder.Reset();
            _builder.AddItem(new MenuItem { Name = "Chicken Katsu", Price = 65.00m, Quantity = 1 });
            _builder.AddItem(new MenuItem { Name = "Mineral Water", Price = 8.00m, Quantity = 2 });
            _builder.SetDeliveryDetails("Jl. Mawar No. 10", false); // Standard delivery
            
            // Tambahkan diskon
            _builder.ApplyCostStrategies(new List<ICostStrategy> { new LoyaltyDiscount() });
            
            return _builder.GetOrder();
        }

        // Metode untuk membuat pesanan khusus (Express tanpa diskon)
        public Order BuildExpressOrder(MenuItem mainItem)
        {
            _builder.Reset();
            _builder.AddItem(mainItem);
            _builder.SetDeliveryDetails("Komplek Dahlia Blok C", true); // Express delivery
            // Tidak ada diskon yang diterapkan
            
            return _builder.GetOrder();
        }
    }
}