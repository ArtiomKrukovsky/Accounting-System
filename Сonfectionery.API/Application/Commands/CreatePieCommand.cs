using Сonfectionery.API.Application.Interfaces;

namespace Сonfectionery.API.Application.Commands
{
    [DataContract]
    public class CreatePieCommand : ICommand<bool>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public IEnumerable<IngredientDto> Ingredients { get; set; }

        [DataMember]
        public IEnumerable<PortionDto> Portions { get; set; }

        public CreatePieCommand(string name, string description, IEnumerable<IngredientDto> ingredients, 
            IEnumerable<PortionDto> portions)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Portions = portions;
        }
    }

    public class CreatePieCommandValidation : IAbstractValidator<CreatePieCommand>
    {
        public CreatePieCommandValidation()
        {
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Description).NotEmpty();
            RuleFor(command => command.Ingredients).Must(ContainIngredients).WithMessage("No ingredients found");
            RuleFor(command => command.Portions).Must(ContainPortions).WithMessage("No portions found");
        }

        private bool ContainIngredients(IEnumerable<IngredientDto> ingredients)
        {
            return orderItems.Any();
        }

        private bool ContainPortions(IEnumerable<PortionDto> portions)
        {
            return orderItems.Any();
        }
    }

    public class CreatePieCommandHandler : IRequestHandler<CreatePieCommand, bool>
    {
        private IPieRepository _pieRepository;
        private ILogger<CreatePieCommand> _logger;

        public CreatePieCommandHandler(IPieRepository pieRepository, ILogger<CreatePieCommand> logger)
        {
            _pieRepository = pieRepository ?? throw new ArgumentNullException(nameof(pieRepository));
            _logger = logger;
        }

        public async Task<bool> Handle(CreatePieCommand request, CancellationToken cancellationToken)
        {
            var pie = Pie.Create();

            _pieRepository.AddAsync(pie);
        }
    }
}
