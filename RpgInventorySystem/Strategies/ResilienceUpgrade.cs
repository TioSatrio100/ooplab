using RpgInventorySystem.Models;
using RpgInventorySystem.Strategies;
using System;

namespace RpgInventorySystem.Strategies
{
    public class ResilienceUpgrade : IUpgradeStrategy
    {
        public string ApplyUpgrade(EquippableItem item)
        {
            if (item is Armor armor)
            {
                armor.Defense += 3;
                armor.Name += " [Resilient]";
                return $"Armor {armor.Name} upgraded! Defense increased by 3.";
            }
            return $"Cannot apply resilience upgrade to {item.Name}.";
        }
    }
}