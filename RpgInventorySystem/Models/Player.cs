using System.Collections.Generic;
using RpgInventorySystem.Interfaces;
using System;
using System.Linq; // Perlu jika Anda menggunakan LINQ di sini

namespace RpgInventorySystem.Models
{
    public class Player
    {
        public string Name { get; set; } = string.Empty; // Inisialisasi default
        public int Health { get; set; } = 100;
        public int ArmorRating { get; set; } = 0;
        public int AttackDamage { get; set; } = 10;
        public List<IItem> Inventory { get; private set; } = new List<IItem>();

        // KONSTRUKTOR YANG HILANG (Perbaikan CS1729)
        public Player(string name)
        {
            Name = name;
        }

        // KONSTRUKTOR DEFAULT (Opsional, jika Anda membuat Player tanpa nama)
        public Player()
        {
            Name = "Guest";
        }
        
        public void DisplayInventory()
        {
            Console.WriteLine($"\n--- {Name}'s Inventory ({Inventory.Count} items) ---");
            foreach (var item in Inventory)
            {
                string status = item is EquippableItem eq && eq.IsEquipped ? " (EQUIPPED)" : "";
                Console.WriteLine($"[{item.Id.ToString().Substring(0, 4)}] {item.Name}{status} - {item.GetDescription()}");
            }
        }
    }
}