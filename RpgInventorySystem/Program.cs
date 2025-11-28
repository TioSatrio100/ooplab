using System;
using RpgInventorySystem.Models;
using RpgInventorySystem.Services;
using RpgInventorySystem.Strategies;
using RpgInventorySystem.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- RPG Inventory Management Demo ---");

        var player = new Player { Name = "Aragorn" };
        var inventoryManager = new InventoryManager();

        // 1. Membuat Item
        var sword = new Weapon("Iron Sword", 100, 15);
        var leatherArmor = new Armor("Leather Tunic", 50, 5);
        var healthPotion = new Potion("Minor Health Potion", 10, 25);
        
        // 2. Menambah ke Inventaris
        inventoryManager.AddItem(player, sword);
        inventoryManager.AddItem(player, leatherArmor);
        inventoryManager.AddItem(player, healthPotion);
        
        player.DisplayInventory();
        
        // 3. Menggunakan Item (IUsable)
        Console.WriteLine("\n--- Usage Test ---");
        player.Health = 50; // Set health rendah
        Console.WriteLine($"Health before use: {player.Health}");
        inventoryManager.UseItem(player, healthPotion.Id);
        Console.WriteLine($"Health after use: {player.Health}");
        
        player.DisplayInventory(); // Potion harusnya hilang

        // 4. Melengkapi Item (IEquippable)
        Console.WriteLine("\n--- Equip Test ---");
        inventoryManager.ToggleEquipItem(player, sword.Id); // Equip
        
        // 5. Upgrade Item (IUpgradeable + Strategy Pattern)
        Console.WriteLine("\n--- Upgrade Test (Strategy Pattern) ---");
        
        // Terapkan strategi upgrade damage ke pedang
        var damageStrategy = new SimpleDamageUpgrade();
        inventoryManager.UpgradeItem(sword, damageStrategy);
        Console.WriteLine($"Sword new damage: {sword.Damage}");
        
        // Terapkan strategi upgrade defense ke baju zirah
        var defenseStrategy = new ResilienceUpgrade();
        inventoryManager.UpgradeItem(leatherArmor, defenseStrategy);
        Console.WriteLine($"Armor new defense: {leatherArmor.Defense}");
        
        player.DisplayInventory();
    }
}