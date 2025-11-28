namespace RpgInventorySystem.Models
{
    public class Armor : EquippableItem
    {
        public int Defense { get; set; }

        // Tambahkan : base(name, value)
        public Armor(string name, int value, int defense)
            : base(name, value) 
        {
            Defense = defense;
        }

        public override string GetDescription() => $"Defense: {Defense}, Value: {Value}";
        
    }
}