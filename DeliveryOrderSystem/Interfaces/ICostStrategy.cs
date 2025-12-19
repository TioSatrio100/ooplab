namespace DeliveryOrderSystem.Interfaces
{
    // Interface dasar untuk strategi yang memodifikasi atau menambah biaya.
    // untuk menghitung biaya tambahan atau diskon
    // Menerima subtotal pesanan dan mengembalikan nilai perubahan (positif untuk biaya, negatif untuk diskon).
    public interface ICostStrategy
    {
        decimal CalculateCost(decimal subTotal);
        
        string Description { get; }
    }
}