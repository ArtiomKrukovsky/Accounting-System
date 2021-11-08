using System;
using System.Collections.Generic;
using System.Linq;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    public class Pie : IAggregateRoot
    {
        private Guid _id;
        private string _name;
        private string _description;
    
        public Portions Portions { get; private set; }

        private readonly List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;

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

        public void UpdateIngredients(IEnumerable<Ingredient> ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException(nameof(ingredients));
            }

            if (!ingredients.Any())
            {
                throw new ArgumentException("Must specified at least one ingredient", nameof(ingredients));
            }

            if (ingredients.Sum(x => x.RelativeAmount) != 1.0)
            {
                throw new ArgumentException("The relarive amount of all ingredients combined must add up to 1.0");
            }

            _ingredients.Clear();
            _ingredients.AddRange(ingredients);
        }
    }
}
