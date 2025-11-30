using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Strategies
{
    // Strategi Biaya Pengiriman Ekspres
    public class ExpressDeliveryFee : ICostStrategy
    {
        private const decimal Fee = 35.00m;
        public string Description => "Express Delivery Fee";

        public decimal CalculateCost(decimal subTotal)
        {
            // Biaya pengiriman ekspres selalu dikenakan
            return Fee;
        }
    }
}