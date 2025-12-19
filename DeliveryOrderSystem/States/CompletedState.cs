using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using System;

namespace DeliveryOrderSystem.States
{
    // Concrete State: Mewakili pesanan yang sudah selesai (tahap akhir).
    public class CompletedState : IOrderState
    {
        public string StatusDescription => "Completed (Order is successfully delivered and finalized)";

        /// <summary>
        /// Pesanan sudah selesai, tidak ada tahap selanjutnya.
        /// </summary>
        public void ProceedToNext(Order order)
        {
            // Pesanan sudah pada status akhir
            Console.WriteLine("Order already Completed. No further states to proceed.");
        }

        /// <summary>
        /// Pesanan yang sudah selesai tidak dapat dibatalkan.
        /// </summary>
        public void CancelOrder(Order order)
        {
            // Melanggar aturan bisnis untuk membatalkan pesanan yang sudah Completed
            Console.WriteLine("ERROR: Cannot cancel an order that is already Completed.");
        }
    }
}