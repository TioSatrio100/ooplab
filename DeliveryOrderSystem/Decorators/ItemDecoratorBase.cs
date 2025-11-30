using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Decorators
{
    public abstract class ItemDecoratorBase : IOrderItem
    {
        protected IOrderItem _wrappedItem;

        public ItemDecoratorBase(IOrderItem item)
        {
            _wrappedItem = item;
        }

        // Metode virtual yang memungkinkan dekorator menimpa atau memanggil kelas dasar.
        public virtual string GetDescription() => _wrappedItem.GetDescription();
        public virtual decimal GetPrice() => _wrappedItem.GetPrice();
        public virtual int GetQuantity() => _wrappedItem.GetQuantity();
    }
}