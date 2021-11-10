using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        private decimal _unitPrice;
        private decimal _discount;
        private int _units;

        private decimal totalPrice => _units * _unitPrice;

        private OrderItem()
        {

        }

        public static OrderItem Create()
        {

        }
    }
}
