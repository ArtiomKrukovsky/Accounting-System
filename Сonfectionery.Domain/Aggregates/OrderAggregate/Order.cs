﻿using System;
using System.Collections.Generic;
using System.Linq;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private string _title;

        private DateTime _orderDate;

        private int _orderStatusId;
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        private Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public static Order Create(string title)
        {
            return new Order
            {
                _title = title,
                _orderStatusId = OrderStatus.Submitted.Id,
                _orderDate = DateTime.UtcNow
            };
        }

        public void AddOrderItem(Guid pieId, decimal unitPrice, decimal discount, int units)
        {
            var existingOrderForPie = _orderItems.FirstOrDefault(x => x.PieId == pieId);

            if (existingOrderForPie != null)
            {
                existingOrderForPie.AddUnits(units);
            }
            else
            {
                var orderItem = OrderItem.Create(pieId, unitPrice, discount, units);
                _orderItems.Add(orderItem);
            }
        }
    }
}
