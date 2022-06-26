using System;
using Newtonsoft.Json;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    [JsonObject]
    public record Portions
    {
        [JsonProperty]
        public int Minimum { get; private set; }
        [JsonProperty]
        public int Maximum { get; private set; }

        public Portions(int minimum, int maximum)
        {
            if (minimum < 1) throw new ArgumentException("Minimum must be above zero.", nameof(minimum));
            if (maximum < 1) throw new ArgumentException("Maximum must be above zero.", nameof(maximum));
            if (maximum <= minimum) throw new ArgumentException("Maximum must be above minimum.", nameof(minimum));

            Minimum = minimum;
            Maximum = maximum;
        }
    }
}