using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Сonfectionery.API.Application.Commands;
using Сonfectionery.API.Application.Queries;
using Сonfectionery.API.Application.ViewModels;

namespace Сonfectionery.API.Controllers
{
    [Route("api/[controller]")]
    public class PieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PieController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("pieId/{pieId}")]
        public async Task<ActionResult<PieViewModel>> GetPieAsync(Guid pieId)
        {
            var pie = await _mediator.Send(new GetPieQuery(pieId));
            return Ok(pie);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostPieAsync([FromBody]CreatePieCommand createPieCommand)
        {
            return await _mediator.Send(createPieCommand);
        }
    }
}
