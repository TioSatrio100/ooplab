using RpgInventorySystem.Interfaces;
using RpgInventorySystem.Strategies;

namespace RpgInventorySystem.Models
{
    public abstract class EquippableItem : IEquippable, IUpgradeable
    {
        public Guid Id { get; } = Guid.NewGuid();
        
        // Diubah menjadi 'set' (public set) agar kelas Strategy dapat memodifikasinya (perbaikan CS0272)
        public string Name { get; set; } 
        public int Value { get; protected set; } // Nilai hanya boleh diatur oleh kelas itu sendiri atau turunannya

        public bool IsEquipped { get; protected set; }
        
        // Dibuat nullable (?) untuk mengatasi warning CS8618 karena boleh null sebelum upgrade
        protected IUpgradeStrategy? _upgradeStrategy; 

        // Konstruktor: diperlukan untuk inisialisasi properti non-nullable (perbaikan CS8618)
        public EquippableItem(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public abstract string GetDescription();

        public void Equip()
        {
            if (!IsEquipped)
            {
                IsEquipped = true;
                Console.WriteLine($"{Name} equipped.");
            }
        }

        public void Unequip()
        {
            if (IsEquipped)
            {
                IsEquipped = false;
                Console.WriteLine($"{Name} unequipped.");
            }
        }

        public void SetUpgradeStrategy(IUpgradeStrategy strategy)
        {
            _upgradeStrategy = strategy;
        }

        public void Upgrade()
        {
            if (_upgradeStrategy == null)
            {
                Console.WriteLine($"Cannot upgrade {Name}: No strategy defined.");
                return;
            }
            // Menerapkan Strategy Pattern
            string result = _upgradeStrategy.ApplyUpgrade(this); 
            Console.WriteLine(result);
        }
    }
}