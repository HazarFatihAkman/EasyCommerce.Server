using EasyCommerce.Server.Interaction.Orders;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter<Order>))]
public class OrderController : ApiController<Order>
{
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Order>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var mediator = await Mediator.Send(new GetOrder.Command { Id = id });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{customerId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Order>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetOrders.Command { CustomerId = customerId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Order>), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostAsync([FromBody] Order order)
    {
        var mediator = await Mediator.Send(new CreateOrder.Command { Order = order });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Order>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Order order)
    {
        var mediator = await Mediator.Send(new UpdateOrder.Command { Order = order });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
