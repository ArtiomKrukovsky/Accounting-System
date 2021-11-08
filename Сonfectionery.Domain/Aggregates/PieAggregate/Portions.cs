using System;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    public class Portions // record
    {
        public int Minimum { get; private set; }
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