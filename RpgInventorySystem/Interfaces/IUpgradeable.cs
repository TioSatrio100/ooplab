using RpgInventorySystem.Strategies;
namespace RpgInventorySystem.Interfaces
{
    public interface IUpgradeable : IItem
    {
        // Strategy Pattern: Upgrade menggunakan Strategy
        void SetUpgradeStrategy(IUpgradeStrategy strategy);
        void Upgrade();
    }
}