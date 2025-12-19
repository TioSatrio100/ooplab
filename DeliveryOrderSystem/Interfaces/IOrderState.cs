namespace DeliveryOrderSystem.Interfaces
{
    public interface IOrderState
    {
    void ProceedToNext(Models.Order order);
    void CancelOrder(Models.Order order);

    string StatusDescription {get;}
    }
}