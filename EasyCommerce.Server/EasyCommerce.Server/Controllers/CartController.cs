using EasyCommerce.Server.Interaction.Carts;
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
[TypeFilter(typeof(ApiExceptionCatcherFilter<Cart>))]
public class CartController : ApiController<Cart>
{
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Cart>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var mediator = await Mediator.Send(new GetCart.Command { Id = id});
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{customerId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Cart>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetCarts.Command { CustomerId = customerId});
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Cart>), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostAsync([FromBody] Cart cart)
    {
        var mediator = await Mediator.Send(new CreateCart.Command { Cart = cart });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Cart>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Cart cart)
    {
        var mediator = await Mediator.Send(new UpdateCart.Command { Cart = cart });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpDelete("delete/{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Cart>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var meditor = await Mediator.Send(new DeleteCart.Command { Id = id });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }
}
