using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using System;

namespace DeliveryOrderSystem.States
{
    // Concrete State: Mewakili pesanan yang sedang disiapkan (Status Awal).
    public class PreparationState : IOrderState
    {
        public string StatusDescription => "Preparation (Items are being prepared and packaged)";

        /// <summary>
        /// Mentransisikan status pesanan dari Preparation ke Delivery.
        /// </summary>
        public void ProceedToNext(Order order)
        {
            Console.WriteLine("Preparation completed. Moving order to Delivery phase.");
            // Transisi Status: Mengubah status Context (Order) menjadi status berikutnya.
            order.CurrentState = new DeliveryState(); 
        }

        /// <summary>
        /// Membatalkan pesanan saat masih dalam tahap Preparation.
        /// </summary>
        public void CancelOrder(Order order)
        {
            Console.WriteLine("Order cancelled from Preparation stage.");
            // Transisi Status: Mengubah status Context (Order) menjadi status Cancelled.
            order.CurrentState = new CancelledState();
        }
    }
}
