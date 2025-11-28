using System;
using System.Linq;
using Xunit;
using RpgInventorySystem.Models;
using RpgInventorySystem.Services;
using RpgInventorySystem.Interfaces;
using RpgInventorySystem.Strategies;

public class InventoryManagerTests
{
    private readonly Player _player;
    private readonly InventoryManager _manager;

    public InventoryManagerTests()
    {
        // Setup dasar yang akan digunakan oleh setiap test (Player dan Manager)
        _player = new Player("TestPlayer"); 
        _manager = new InventoryManager();
    }

    // --- TEST FUNGSIONALITAS DASAR (ADD/REMOVE) ---

    [Fact]
    public void AddItem_ShouldIncreaseInventoryCount()
    {
        // Arrange
        var potion = new Potion("Healing Potion", 10, 50);

        // Act
        _manager.AddItem(_player, potion);

        // Assert
        Assert.Single(_player.Inventory);
    }

    [Fact]
    public void RemoveItem_ShouldDecreaseInventoryCount()
    {
        // Arrange
        var sword = new Weapon("Old Sword", 100, 10);
        _manager.AddItem(_player, sword);

        // Act
        _manager.RemoveItem(_player, sword.Id);

        // Assert
        Assert.Empty(_player.Inventory);
    }
    
    // --- TEST FUNGSIONALITAS IUSABLE (POTION) ---

    [Fact]
    public void UseItem_Potion_ShouldRestoreHealthAndBeConsumed()
    {
        // Arrange
        var potion = new Potion("Minor HP Potion", 5, 20);
        _player.Health = 50; // Set health rendah
        _manager.AddItem(_player, potion);

        // Act
        _manager.UseItem(_player, potion.Id);

        // Assert
        Assert.Equal(70, _player.Health); // 50 + 20
        Assert.Empty(_player.Inventory); // Potion harus hilang setelah digunakan
    }

    // --- TEST FUNGIONALITAS IEQUIPPABLE ---

    [Fact]
    public void ToggleEquipItem_ShouldChangeIsEquippedStatus()
    {
        // Arrange
        var armor = new Armor("Plate Armor", 500, 50);
        _manager.AddItem(_player, armor);

        // Act: Equip
        _manager.ToggleEquipItem(_player, armor.Id);
        
        // Assert
        var equippedArmor = _player.Inventory.First() as EquippableItem;
        Assert.True(equippedArmor.IsEquipped);

        // Act: Unequip
        _manager.ToggleEquipItem(_player, armor.Id);
        
        // Assert
        Assert.False(equippedArmor.IsEquipped);
    }

    // --- TEST FUNGIONALITAS IUPGRADEABLE (STRATEGY PATTERN) ---

    [Fact]
    public void UpgradeItem_SimpleDamageStrategy_ShouldIncreaseWeaponDamage()
    {
        // Arrange
        var sword = new Weapon("Iron Sword", 100, 15);
        _manager.AddItem(_player, sword);
        var initialDamage = sword.Damage;
        var strategy = new SimpleDamageUpgrade();

        // Act
        _manager.UpgradeItem(sword, strategy); // Memanggil Upgrade Strategy

        // Assert
        Assert.Equal(initialDamage + 5, sword.Damage);
        Assert.Contains("+1", sword.Name); // Verifikasi nama diubah
    }

    [Fact]
    public void UpgradeItem_ResilienceStrategy_ShouldIncreaseArmorDefense()
    {
        // Arrange
        var armor = new Armor("Leather Tunic", 50, 5);
        _manager.AddItem(_player, armor);
        var initialDefense = armor.Defense;
        var strategy = new ResilienceUpgrade();

        // Act
        _manager.UpgradeItem(armor, strategy);

        // Assert
        Assert.Equal(initialDefense + 3, armor.Defense);
        Assert.Contains("[Resilient]", armor.Name); // Verifikasi nama diubah
    }
}
