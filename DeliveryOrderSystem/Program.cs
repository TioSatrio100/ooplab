using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Strategies;
using DeliveryOrderSystem.Builders;
using DeliveryOrderSystem.Decorators; 
using DeliveryOrderSystem.Factories; // Tambahkan ini
using System;
using DeliveryOrderSystem.Interfaces; 
using System.Collections.Generic;

namespace DeliveryOrderSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {

            StandardOrderBuilder builder = new StandardOrderBuilder();
            OrderDirector director = new OrderDirector(builder);

            Console.WriteLine("\n---Delivery System---");
            
            // 1. Inisialisasi Pabrik Budget
            IMenuFactory budgetFactory = new BudgetMenuFactory();
            Console.WriteLine("Budget Menu");
            
            // Buat pesanan Express menggunakan item dari Budget Factory
            // menggunakan director.BuildExpressOrder, yang menerima MenuItem
            Order budgetOrder = director.BuildExpressOrder(budgetFactory.CreateMainDish());
            
            // 3. Tambahkan item lain dari factory yang sama
            budgetOrder.ItemsAsOrderItems.Add(budgetFactory.CreateBeverage());
            
            Console.WriteLine("Budget Order Items:");
            foreach (var item in budgetOrder.ItemsAsOrderItems)
            {
                Console.WriteLine($"- {item.GetDescription()} ({item.GetPrice():C2})");
            }
            budgetOrder.CalculateTotalCost();

            // Beralih ke Pabrik Gourmet dengan mudah (tanpa mengubah kode konstruksi pesanan)
            IMenuFactory gourmetFactory = new GourmetMenuFactory();
            Console.WriteLine("Gourmet Menu");

            Order gourmetOrder = director.BuildExpressOrder(gourmetFactory.CreateMainDish());
            gourmetOrder.ItemsAsOrderItems.Add(gourmetFactory.CreateSideDish());
            
            Console.WriteLine("Gourmet Order Items:");
            foreach (var item in gourmetOrder.ItemsAsOrderItems)
            {
                Console.WriteLine($"- {item.GetDescription()} ({item.GetPrice():C2})");
            }
            gourmetOrder.CalculateTotalCost();
            
        }
    }
}