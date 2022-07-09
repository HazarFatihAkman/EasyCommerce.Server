using EasyCommerce.Server.Interaction.Taxes;
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
[TypeFilter(typeof(ApiExceptionCatcherFilter<Tax>))]
public class TaxController : ApiController<Tax>
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Tax>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var mediator = await Mediator.Send(new GetTaxes.Command { });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Tax>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var meditor = await Mediator.Send(new GetTax.Command { Id = id });
        return StatusCode(meditor.ResponseStatusCode,meditor);
    }
    
    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Tax>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] Tax tax)
    {
        var mediator = await Mediator.Send(new CreateTax.Command { Tax = tax });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Tax>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Tax tax)
    {
        var mediator = await Mediator.Send(new UpdateTax.Command { Tax = tax });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
