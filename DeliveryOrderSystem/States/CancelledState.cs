using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using System;

namespace DeliveryOrderSystem.States
{
    public class CancelledState : IOrderState
    {
        public string StatusDescription => "Cancelled";

        public void ProceedToNext(Order order)
        {
            Console.WriteLine($"-> ERROR: Cannot proceed with a cancelled order.");
            // Tidak ada perubahan status
        }

        public void CancelOrder(Order order)
        {
            Console.WriteLine($"-> Order {order.Id.ToString().Substring(0, 4)} is already cancelled.");
            // Tidak ada perubahan status
        }
    }
}