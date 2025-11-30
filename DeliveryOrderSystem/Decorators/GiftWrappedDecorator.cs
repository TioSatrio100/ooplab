using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Decorators
{
    public class GiftWrappedDecorator : ItemDecoratorBase
    {
        private const decimal GiftWrapCost = 15.00m;

        public GiftWrappedDecorator(IOrderItem item) : base(item) { }

        public override string GetDescription()
        {
            return $"Gift Wrapped: {_wrappedItem.GetDescription()}";
        }

        public override decimal GetPrice()
        {
            // Menambahkan biaya kado ke harga item dasar
            return _wrappedItem.GetPrice() + GiftWrapCost;
        }
    }
}