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
            // ... (Kode demo State, Strategy, Decorator, Builder yang sudah ada)

            StandardOrderBuilder builder = new StandardOrderBuilder();
            OrderDirector director = new OrderDirector(builder);

            // --- DEMO 5: Abstract Factory Pattern ---
            Console.WriteLine("\n--- Abstract Factory Demo ---");
            
            // 1. Inisialisasi Pabrik Budget
            IMenuFactory budgetFactory = new BudgetMenuFactory();
            Console.WriteLine("[Factory] Menggunakan Budget Menu Factory.");
            
            // 2. Buat pesanan Express menggunakan item dari Budget Factory
            // Note: Kita menggunakan director.BuildExpressOrder, yang menerima MenuItem
            Order budgetOrder = director.BuildExpressOrder(budgetFactory.CreateMainDish());
            
            // 3. Tambahkan item lain dari factory yang sama
            budgetOrder.ItemsAsOrderItems.Add(budgetFactory.CreateBeverage());
            
            Console.WriteLine("Budget Order Items:");
            foreach (var item in budgetOrder.ItemsAsOrderItems)
            {
                Console.WriteLine($"- {item.GetDescription()} ({item.GetPrice():C2})");
            }
            budgetOrder.CalculateTotalCost();

            // 4. Beralih ke Pabrik Gourmet dengan mudah (tanpa mengubah kode konstruksi pesanan)
            IMenuFactory gourmetFactory = new GourmetMenuFactory();
            Console.WriteLine("[Factory] Beralih ke Gourmet Menu Factory.");

            Order gourmetOrder = director.BuildExpressOrder(gourmetFactory.CreateMainDish());
            gourmetOrder.ItemsAsOrderItems.Add(gourmetFactory.CreateSideDish());
            
            Console.WriteLine("Gourmet Order Items:");
            foreach (var item in gourmetOrder.ItemsAsOrderItems)
            {
                Console.WriteLine($"- {item.GetDescription()} ({item.GetPrice():C2})");
            }
            gourmetOrder.CalculateTotalCost();
            
            // ... (Lanjutkan demo State Pattern, jika ada)
        }
    }
}