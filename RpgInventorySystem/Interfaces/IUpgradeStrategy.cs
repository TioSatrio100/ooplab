using RpgInventorySystem.Models;

namespace RpgInventorySystem.Strategies
{
    // Strategy Pattern dan SRP: Setiap strategi hanya fokus pada satu jenis upgrade
    public interface IUpgradeStrategy
    {
        string ApplyUpgrade(EquippableItem item);
    }
}