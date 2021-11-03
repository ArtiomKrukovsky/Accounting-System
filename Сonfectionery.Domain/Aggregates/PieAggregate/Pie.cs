using System;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    public class Pie
    {
        private Guid _id;
        private string _name;
        private string _description;
        public Portions Portions;

        private Pie()
        {

        }

        public static Pie Create(string name, string description, Portions portions)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (portions == null)
            {
                throw new ArgumentNullException(nameof(portions));
            }

            return new Pie
            {
                _id = Guid.NewGuid(),
                _name = name,
                _description = description,
                Portions = portions
            };
        }
    }
}
