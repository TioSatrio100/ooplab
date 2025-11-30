using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Strategies;
using System; 
using System.Collections.Generic;

namespace DeliveryOrderSystem.Builders
{
    // Concrete Builder untuk merakit objek Order.
    // Mengimplementasikan IOrderBuilder yang telah diupdate untuk menerima IOrderItem.
    public class StandardOrderBuilder : IOrderBuilder
    {
        private Order _order = new Order();

        public StandardOrderBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._order = new Order();
        }

        // Kritis: Metode ini sekarang menerima IOrderItem, bukan MenuItem,
        // untuk mendukung item yang didekorasi (Decorator Pattern).
        public void AddItem(IOrderItem item) 
        {
            // Menambahkan item yang didekorasi atau item dasar ke daftar pesanan
            this._order.ItemsAsOrderItems.Add(item); 
        }

        public void SetDeliveryDetails(string address, bool isExpress)
        {
            // Hapus strategi pengiriman lama jika ada
            this._order.CostStrategies.RemoveAll(s => s is StandardDeliveryFee || s is ExpressDeliveryFee);

            // Menerapkan Strategy Pengiriman (Strategy Pattern)
            if (isExpress)
            {
                this._order.CostStrategies.Add(new ExpressDeliveryFee());
                Console.WriteLine($"[Builder] Applied Express Delivery fee.");
            }
            else
            {
                this._order.CostStrategies.Add(new StandardDeliveryFee());
                Console.WriteLine($"[Builder] Applied Standard Delivery fee.");
            }
        }

        public void ApplyCostStrategies(List<ICostStrategy> strategies)
        {
            // Menambahkan Strategi biaya/diskon tambahan
            this._order.CostStrategies.AddRange(strategies);
        }

        public Order GetOrder()
        {
            // Logika validasi: Memastikan pesanan memiliki item
            if (this._order.ItemsAsOrderItems.Count == 0)
            {
                throw new InvalidOperationException("Cannot build an order without any items.");
            }
            
            Order finalOrder = this._order;
            
            // Reset builder untuk persiapan pembuatan pesanan berikutnya
            this.Reset(); 
            
            return finalOrder;
        }
    }
}