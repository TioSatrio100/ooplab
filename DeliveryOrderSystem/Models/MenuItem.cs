using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Models
{
    // MenuItem sekarang adalah komponen konkret yang dapat didekorasi.
    public class MenuItem : IOrderItem
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // --- Implementasi IOrderItem ---

        /// <summary>
        /// Mengembalikan deskripsi dasar item (Nama dan Kuantitas).
        /// </summary>
        public string GetDescription()
        {
            return $"{Name} x{Quantity}";
        }

        /// <summary>
        /// Menghitung harga total item (Harga per unit * Kuantitas).
        /// </summary>
        public decimal GetPrice()
        {
            return Price * Quantity;
        }

        /// <summary>
        /// Mengembalikan kuantitas item.
        /// </summary>
        public int GetQuantity()
        {
            return Quantity;
        }
    }
}