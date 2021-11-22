using System;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class OrderItem : Entity
    {
        public Guid PieId { get; private set; }

        private decimal _unitPrice;
        private decimal _discount;
        private int _units;

        private decimal totalPrice => _units * _unitPrice;

        private OrderItem()
        {

        }

        public static OrderItem Create(Guid pieId, decimal unitPrice, decimal discount, int units = 1)
        {
            if (units <= 0)
            {
                throw new ArgumentException("Invalid number of units");
            }

            if ((unitPrice * units) < discount)
            {
                throw new ArgumentException("The total of order item is lower than applied discount");
            }

            return new OrderItem
            {
                PieId = pieId,
                _unitPrice = unitPrice,
                _discount = discount
            };
        }
    }
}
