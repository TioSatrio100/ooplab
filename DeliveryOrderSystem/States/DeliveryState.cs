using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using System;

namespace DeliveryOrderSystem.States
{
    public class DeliveryState : IOrderState
    {
        public string StatusDescription => "Out For Delivery";

        public void ProceedToNext(Order order)
        {
            Console.WriteLine($"-> Order {order.Id.ToString().Substring(0, 4)} has been delivered!");
            // Transisi ke status berikutnya yang valid
            order.CurrentState = new CompletedState();
        }

        public void CancelOrder(Order order)
        {
            Console.WriteLine($"-> WARNING: Cannot cancel order {order.Id.ToString().Substring(0, 4)}: already out for delivery.");
            // Tidak ada perubahan status
        }
    }
}