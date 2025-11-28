namespace RpgInventorySystem.Models
{
    public class Weapon : EquippableItem
    {
        public int Damage { get; set; }

        // Tambahkan : base(name, value)
        public Weapon(string name, int value, int damage)
            : base(name, value) 
        {
            Damage = damage;
        }

        public override string GetDescription() => $"Damage: {Damage}, Value: {Value}";

    }
}