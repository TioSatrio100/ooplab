using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Interfaces; // Wajib: Untuk IOrderItem
using System.Collections.Generic;

namespace DeliveryOrderSystem.Builders
{
    public interface IOrderBuilder
    {
        void Reset();
        
        // PERBAIKAN KRITIS: Metode ini harus menerima IOrderItem (sesuai dengan StandardOrderBuilder.cs)
        void AddItem(IOrderItem item); 

        void SetDeliveryDetails(string address, bool isExpress);

        void ApplyCostStrategies(List<ICostStrategy> strategies);

        Order GetOrder();
    }
}