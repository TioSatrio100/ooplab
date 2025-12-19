using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Strategies
{
    // Concrete Strategy: Implementasi diskon loyalitas.
    public class LoyaltyDiscount : ICostStrategy
    {
        // Konstanta untuk tingkat diskon (misalnya, 10% atau 0.10)
        private const decimal DiscountRate = -0.10m; 

        // Properti deskripsi untuk output log atau tampilan UI
        public string Description => "10% Loyalty Discount";

        /// <summary>
        /// Menghitung penyesuaian biaya (diskon) berdasarkan subtotal.
        /// </summary>
        /// <param name="subTotal">Total harga item sebelum diskon dan biaya kirim.</param>
        /// <returns>Nilai diskon (negatif) yang akan diterapkan ke total biaya.</returns>
        public decimal CalculateCost(decimal subTotal)
        {
            // Diskon selalu 10% dari subtotal
            return subTotal * DiscountRate;
        }
    }
}