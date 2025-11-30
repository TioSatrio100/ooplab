using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Factories
{
    // Concrete Factory 1: Membuat item menu yang mahal (Gourmet)
    public class GourmetMenuFactory : IMenuFactory
    {
        public MenuItem CreateMainDish()
        {
            return new MenuItem { Name = "Wagyu Steak A5", Price = 350.00m, Quantity = 1 };
        }

        public MenuItem CreateSideDish()
        {
            return new MenuItem { Name = "Truffle Mushroom Soup", Price = 65.00m, Quantity = 1 };
        }

        public MenuItem CreateBeverage()
        {
            return new MenuItem { Name = "Premium Black Coffee", Price = 30.00m, Quantity = 1 };
        }
    }
}