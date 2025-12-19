using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Strategies
{
    // Concrete Strategy: Implementasi biaya kirim standar (dengan logika batas minimum).
    public class StandardDeliveryFee : ICostStrategy
    {
        // Biaya dasar untuk pengiriman standar
        private const decimal Fee = 15.00m; 
        
        // Batas subtotal agar pengiriman menjadi gratis
        private const decimal FreeDeliveryThreshold = 100.00m;

        public string Description => "Standard Delivery Fee (Free over 100)";

        /// <summary>
        /// Menghitung biaya pengiriman standar. Biaya dihilangkan jika subtotal mencapai batas.
        /// </summary>
        /// <param name="subTotal">Total harga item sebelum diskon dan biaya kirim.</param>
        /// <returns>Nilai biaya (positif) yang akan ditambahkan ke total biaya.</returns>
        public decimal CalculateCost(decimal subTotal)
        {
            // Logika kondisional: Jika subtotal >= 100, biaya kirim 0.00.
            if (subTotal >= FreeDeliveryThreshold)
            {
                return 0.00m;
            }
            else
            {
                return Fee;
            }
        }
    }
}