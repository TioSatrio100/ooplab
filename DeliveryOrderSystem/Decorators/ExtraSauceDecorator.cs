using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Decorators
{
    public class ExtraSauceDecorator : ItemDecoratorBase
    {
        private const decimal SauceCost = 5.00m;

        public ExtraSauceDecorator(IOrderItem item) : base(item) { }

        public override string GetDescription()
        {
            return $"{_wrappedItem.GetDescription()} + Extra Sauce";
        }

        public override decimal GetPrice()
        {
            // Menambahkan biaya saus ke harga item dasar
            return _wrappedItem.GetPrice() + SauceCost;
        }
    }
}