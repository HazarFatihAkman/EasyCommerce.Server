using EasyCommerce.Server.Interaction.Prices;
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
[TypeFilter(typeof(ApiExceptionCatcherFilter<Price>))]
public class PriceController : ApiController<Price>
{
    [HttpGet("product/{productId:guid}/prices")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Price>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid productId)
    {
        var meditor = await Mediator.Send(new GetPrices.Command { ProductId = productId });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }

    [HttpPut("product/{productId:guid}/price/update")]
    [ProducesResponseType(typeof(ApiResponse<Price>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid productId,[FromBody] Price price)
    {
        var meditor = await Mediator.Send(new UpdatePrice.Command { ProductId = productId, Price = price });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }

    [HttpPut("product/{productId:guid}/price/create")]
    [ProducesResponseType(typeof(ApiResponse<Price>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromRoute] Guid productId,[FromBody] Price price)
    {
        var meditor = await Mediator.Send(new CreatePrice.Command { ProductId = productId, Price = price });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }
}
