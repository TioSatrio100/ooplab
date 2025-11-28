using RpgInventorySystem.Models;
using RpgInventorySystem.Strategies;
using System;

namespace RpgInventorySystem.Strategies
{
    public class SimpleDamageUpgrade : IUpgradeStrategy
    {
        public string ApplyUpgrade(EquippableItem item)
        {
            // Liskov Substitution Principle (LSP): Memastikan item adalah Weapon
            if (item is Weapon weapon)
            {
                weapon.Damage += 5;
                weapon.Name += "+1";
                return $"Weapon {weapon.Name} upgraded! Damage increased by 5.";
            }
            return $"Cannot apply damage upgrade to {item.Name}.";
        }
    }
}