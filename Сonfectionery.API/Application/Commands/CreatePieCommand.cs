using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.Constants;
using Сonfectionery.API.Application.DTOs;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.Domain.Aggregates.PieAggregate;
using Сonfectionery.Services.Kafka;

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
        public PortionsDto Portions { get; set; }

        [DataMember]
        public IEnumerable<IngredientDto> Ingredients { get; set; }

        public CreatePieCommand(string name, string description, PortionsDto portions,
            IEnumerable<IngredientDto> ingredients)
        {
            Name = name;
            Description = description;
            Portions = portions;
            Ingredients = ingredients;
        }
    }

    public class CreatePieCommandValidation : AbstractValidator<CreatePieCommand>
    {
        public CreatePieCommandValidation()
        {
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.Description).NotEmpty();
            RuleFor(command => command.Ingredients).Must(ContainIngredients).WithMessage("No ingredients found");
            RuleFor(command => command.Portions).NotNull().Must(BeValidPortions).WithMessage("Invalid portion values");
        }

        private static bool ContainIngredients(IEnumerable<IngredientDto> ingredients)
        {
            return ingredients.Any();
        }

        private static bool BeValidPortions(PortionsDto portions)
        {
            return portions.Maximum > 0 && portions.Minimum > 0 && portions.Minimum < portions.Maximum;
        }
    }

    public class CreatePieCommandHandler : IRequestHandler<CreatePieCommand, bool>
    {
        private readonly IPieRepository _pieRepository;
        private readonly IKafkaService<Pie> _kafkaService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePieCommand> _logger;

        public CreatePieCommandHandler(
            IPieRepository pieRepository,
            IKafkaService<Pie> kafkaService,
            IMapper mapper, 
            ILogger<CreatePieCommand> logger)
        {
            _pieRepository = pieRepository ?? throw new ArgumentNullException(nameof(pieRepository));
            _kafkaService = kafkaService ?? throw new ArgumentNullException(nameof(kafkaService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Pie - Pie: {@Name}", request.Name);

            var portions = _mapper.Map<Portions>(request.Portions);
            var pie = Pie.Create(request.Name, request.Description, portions);

            var ingredients = _mapper.Map<IEnumerable<Ingredient>>(request.Ingredients);
            pie.UpdateIngredients(ingredients);

            _logger.LogInformation("----- Posting Pie in the SQL DB - Pie: {@Pie}", pie);

            await _pieRepository.AddAsync(pie);

            _logger.LogInformation("----- Sending Pie in Kafka - Pie: {@Pie}", pie);

            const string piesTopic = KafkaConstants.PiesTopic;
            const string pieKey = KafkaConstants.PieKey;

            await _kafkaService.ProduceAsync(piesTopic, pieKey, pie);

            return true;
        }
    }
}
