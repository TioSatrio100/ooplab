using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.States; 
using System;
using System.Collections.Generic;
using System.Linq; 

namespace DeliveryOrderSystem.Models
{
    public class Order
    {
        public Guid Id { get; } = Guid.NewGuid();
        
        // ItemsAsOrderItems: Mendukung DECORATOR PATTERN.
        // List ini menampung IOrderItem, memungkinkan item dasar (MenuItem) atau item yang didekorasi (ExtraSauceDecorator).
        public List<IOrderItem> ItemsAsOrderItems { get; set; } = new List<IOrderItem>(); 
        
        // CostStrategies: Mendukung STRATEGY PATTERN.
        // List ini menampung algoritma biaya/diskon yang akan diterapkan secara fleksibel.
        public List<ICostStrategy> CostStrategies { get; set; } = new List<ICostStrategy>(); 
        
        // --- Implementasi STATE PATTERN ---
        private IOrderState _currentState; 

        public IOrderState CurrentState 
        {
            get => _currentState;
            set
            {
                _currentState = value;
                Console.WriteLine($"Order {Id.ToString().Substring(0, 4)} is now in state: {_currentState.StatusDescription}");
            }
        }
        
        // Konstruktor
        public Order()
        {
            // Perbaikan CS8618: Inisialisasi field privat _currentState langsung di konstruktor.
            _currentState = new PreparationState(); 
            Console.WriteLine($"Order {Id.ToString().Substring(0, 4)} created in state: {_currentState.StatusDescription}");
        }

        // Metode delegasi untuk transisi State
        public void ProceedToNextState()
        {
            // Mendelegasikan permintaan ke objek status saat ini
            _currentState.ProceedToNext(this);
        }

        public void Cancel()
        {
            // Mendelegasikan permintaan pembatalan ke objek status saat ini
            _currentState.CancelOrder(this);
        }
        
        // --- Implementasi STRATEGY PATTERN ---
        public decimal CalculateTotalCost()
        {
            // 1. Hitung Subtotal dari semua IOrderItem (harga item didekorasi akan dihitung di sini)
            decimal subTotal = ItemsAsOrderItems.Sum(item => item.GetPrice()); 
            decimal finalCost = subTotal;
            
            Console.WriteLine($"\n[Cost Calculation] Subtotal: {subTotal:C2}");

            // 2. Terapkan setiap strategi biaya yang melekat pada pesanan
            foreach (var strategy in CostStrategies)
            {
                decimal costAdjustment = strategy.CalculateCost(subTotal);
                finalCost += costAdjustment;
                
                string sign = costAdjustment >= 0 ? "+" : "";
                Console.WriteLine($"[Cost Calculation] {strategy.Description}: {sign}{costAdjustment:C2}");
            }

            Console.WriteLine($"[Cost Calculation] Total Final Cost: {finalCost:C2}\n");
            return finalCost;
        }
    }
}