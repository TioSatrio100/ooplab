using global::RpgInventorySystem.Interfaces;
using global::RpgInventorySystem.Models;

namespace RpgInventorySystem.Models
{
    public class Potion : IUsable
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public int Value { get; }
        public int HealthRestoreAmount { get; }

        public Potion(string name, int value, int healthRestore)
        {
            Name = name;
            Value = value;
            HealthRestoreAmount = healthRestore;
        }

        public string GetDescription() => $"Restores {HealthRestoreAmount} HP. Value: {Value}";

        public string Use(Player player)
        {
            // SRP: Item hanya berinteraksi dengan Player, bukan mengelola inventaris
            player.Health += HealthRestoreAmount;
            if (player.Health > 100) player.Health = 100; // Cap Health
            
            // Logika untuk menghapus item dari inventaris harus ada di Service
            return $"{player.Name} used {Name} and restored {HealthRestoreAmount} HP. Current Health: {player.Health}";
        }
    }
}