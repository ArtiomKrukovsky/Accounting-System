﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Сonfectionery.API.Application.DTOs;
using Сonfectionery.API.Application.Interfaces;
using Сonfectionery.Domain.Aggregates.PieAggregate;

namespace Сonfectionery.API.Application.Commands
{
    public class CreatePieCommand : ICommand<bool>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public PortionsDto Portions { get; set; }

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
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePieCommand> _logger;

        public CreatePieCommandHandler(
            IPieRepository pieRepository,
            IMapper mapper, 
            ILogger<CreatePieCommand> logger)
        {
            _pieRepository = pieRepository ?? throw new ArgumentNullException(nameof(pieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(CreatePieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("----- Creating Pie - Pie: {@Name}", request.Name);

            var portions = _mapper.Map<Portions>(request.Portions);
            var pie = new Pie(request.Name, request.Description, portions);

            var ingredients = _mapper.Map<IEnumerable<Ingredient>>(request.Ingredients);
            pie.UpdateIngredients(ingredients);

            _logger.LogInformation("----- Posting Pie in the SQL DB - Pie: {@Pie}", pie);

            await _pieRepository.AddAsync(pie);
            await _pieRepository.UnitOfWork.CommitAsync(cancellationToken);

            return true;
        }
    }
}
