using RpgInventorySystem.Interfaces; 
using RpgInventorySystem.Models;    
using System;

namespace RpgInventorySystem.Models
{
    public interface IUsable : IItem
    {
        // Open/Closed Principle (OCP): Item dapat menentukan perilaku penggunaannya sendiri
        string Use(Player player);
    }
}