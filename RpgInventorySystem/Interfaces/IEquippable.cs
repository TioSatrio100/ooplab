namespace RpgInventorySystem.Interfaces
{
    public interface IEquippable : IItem
    {
        void Equip();
        void Unequip();
    }
}