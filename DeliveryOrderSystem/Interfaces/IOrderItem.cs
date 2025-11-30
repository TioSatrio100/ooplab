namespace DeliveryOrderSystem.Interfaces
{
    public interface IOrderItem
    {
        string GetDescription();
        decimal GetPrice();
        int GetQuantity();
    }
}