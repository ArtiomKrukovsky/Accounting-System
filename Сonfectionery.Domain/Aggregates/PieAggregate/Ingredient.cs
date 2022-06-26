using System;
using Newtonsoft.Json;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    [JsonObject]
    public record Ingredient
    {
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public bool IsAllergen { get; private set; }
        [JsonProperty]
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
