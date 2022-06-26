using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Сonfectionery.Domain.Events.DomainEvents;
using Сonfectionery.Domain.Seedwork;

namespace Сonfectionery.Domain.Aggregates.PieAggregate
{
    [JsonObject]
    public class Pie : Entity, IAggregateRoot
    {
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public Portions Portions { get; private set; }

        [JsonProperty]
        private readonly List<Ingredient> _ingredients;
        public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;

        private Pie()
        {
            _ingredients = new List<Ingredient>();
        }

        public Pie(string name, string description, Portions portions) : this()
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

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Portions = portions;

            AddDomainEvent(new PieCreatedEvent(this));
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
