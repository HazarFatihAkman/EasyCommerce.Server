using EasyCommerce.Server.Interaction.CreditCards;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter<CreditCard>))]
public class CreditCardController : ApiController<CreditCard>
{
    [HttpGet("{customerId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<CreditCard>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetCreditCardsByCustomerId.Command { CustomerId = customerId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<CreditCard>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] CreditCard creditCard)
    {
        var mediator = await Mediator.Send(new CreateCreditCard.Command { CreditCard = creditCard });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<CreditCard>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] CreditCard creditCard)
    {
        var mediator = await Mediator.Send(new UpdateCreditCard.Command { CreditCard = creditCard });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpDelete("delete/{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<CreditCard>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var mediator = await Mediator.Send(new DeleteCreditCard.Command { Id = id });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
