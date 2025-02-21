﻿using System;
using System.Collections.Generic;
using System.Linq;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus Paid = new(2, nameof(Paid).ToLowerInvariant());
        public static OrderStatus Cooking = new(3, nameof(Cooking).ToLowerInvariant());
        public static OrderStatus Shipping = new(4, nameof(Shipping).ToLowerInvariant());
        public static OrderStatus Cancelled = new(5, nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<OrderStatus> List() => new List<OrderStatus> { Submitted, Paid, Cooking, Shipping, Cancelled };

        public static OrderStatus FromIdentifier(int id)
        {
            var state = List().FirstOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
