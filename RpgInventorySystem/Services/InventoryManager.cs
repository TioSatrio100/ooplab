using RpgInventorySystem.Models;
using RpgInventorySystem.Interfaces;
using RpgInventorySystem.Strategies;
using System;
using System.Linq;

namespace RpgInventorySystem.Services
{
    // SRP: Fokus hanya pada manajemen inventaris dan interaksi item
    // DIP: Bergantung pada abstraksi (IItem, IUsable, IEquippable)
    public class InventoryManager
    {
        public void AddItem(Player player, IItem item)
        {
            player.Inventory.Add(item);
            Console.WriteLine($"{player.Name} obtained {item.Name}.");
        }

        public void RemoveItem(Player player, Guid itemId)
        {
            var item = player.Inventory.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                player.Inventory.Remove(item);
                Console.WriteLine($"{player.Name} removed {item.Name}.");
            }
        }

        // Metode untuk menggunakan item (menggunakan IUsable)
        public void UseItem(Player player, Guid itemId)
        {
            var item = player.Inventory.FirstOrDefault(i => i.Id == itemId);
            
            if (item is IUsable usableItem)
            {
                string result = usableItem.Use(player);
                Console.WriteLine(result);

                // Setelah digunakan, hapus jika itu bukan Quest Item (misalnya)
                if (item is Potion)
                {
                    player.Inventory.Remove(item);
                    Console.WriteLine($"{item.Name} consumed.");
                }
            }
            else
            {
                Console.WriteLine($"Item {item?.Name ?? "Unknown"} cannot be used.");
            }
        }

        // Metode untuk equip/unequip item
        public void ToggleEquipItem(Player player, Guid itemId)
        {
            var item = player.Inventory.FirstOrDefault(i => i.Id == itemId);

            if (item is IEquippable equippable)
            {
                if (equippable is EquippableItem eq)
                {
                    if (eq.IsEquipped)
                    {
                        equippable.Unequip();
                        Console.WriteLine($"{item.Name} unequipped.");
                    }
                    else
                    {
                        equippable.Equip();
                        Console.WriteLine($"{item.Name} equipped.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Item {item?.Name ?? "Unknown"} cannot be equipped.");
            }
        }

        // Metode untuk upgrade item
        public void UpgradeItem(IUpgradeable item, IUpgradeStrategy strategy)
        {
            item.SetUpgradeStrategy(strategy);
            item.Upgrade();
        }
    }
}