using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Interfaces
{
    // Abstract Factory: Mendefinisikan antarmuka untuk membuat keluarga objek.
    public interface IMenuFactory
    {
        MenuItem CreateMainDish();
        MenuItem CreateSideDish();
        MenuItem CreateBeverage();
    }
}