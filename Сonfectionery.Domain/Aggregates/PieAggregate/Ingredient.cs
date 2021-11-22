﻿using System;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    public record Ingredient
    {
        public string Name { get; private set; }
        public bool IsAllergen { get; private set; }
        public double RelativeAmount { get; private set; }

        public Ingredient(string name, bool isAllergen, double relativeAmount)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (relativeAmount is > 1.0 or <= 0.0) throw new ArgumentOutOfRangeException(nameof(relativeAmount));

            Name = name;
            IsAllergen = isAllergen;
            RelativeAmount = relativeAmount;
        }
    }
}
