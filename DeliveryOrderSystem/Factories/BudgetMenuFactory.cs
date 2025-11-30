using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Factories
{
    // Concrete Factory 2: Membuat item menu yang ekonomis (Budget)
    public class BudgetMenuFactory : IMenuFactory
    {
        public MenuItem CreateMainDish()
        {
            return new MenuItem { Name = "Nasi Ayam Geprek", Price = 22.00m, Quantity = 1 };
        }

        public MenuItem CreateSideDish()
        {
            return new MenuItem { Name = "Tahu Goreng (2 pcs)", Price = 5.00m, Quantity = 1 };
        }

        public MenuItem CreateBeverage()
        {
            return new MenuItem { Name = "Es Teh Manis", Price = 7.00m, Quantity = 1 };
        }
    }
}