using System;

namespace RpgInventorySystem.Interfaces
{
    // Single Responsibility Principle (SRP): Hanya mendefinisikan item dasar
    public interface IItem
    {
        Guid Id { get; }
        string Name { get; }
        int Value { get; }
        string GetDescription();
    }
}