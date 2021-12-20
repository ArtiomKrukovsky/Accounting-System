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
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("orderId/{orderId}")]
        public async Task<ActionResult<OrderViewModel>> GetOrderAsync(Guid orderId)
        {
            var order = await _mediator.Send(new GetOrderQuery(orderId));
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEquatable<OrderViewModel>>> GetOrdersAsync()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostOrderAsync([FromBody]CreateOrderCommand createOrderCommand)
        {
            return await _mediator.Send(createOrderCommand);
        }
    }
}
