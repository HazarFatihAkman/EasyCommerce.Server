using EasyCommerce.Server.Interaction.Addresses;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter<Address>))]
public class AddressController : ApiController<Address>
{
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Address>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFromIdAsync([FromRoute] Guid id)
    {
        var mediator = await Mediator.Send(new GetAddressFromId.Command { Id = id });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{customerId:Guid}/address")]
    [ProducesResponseType(typeof(ApiResponse<Address>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFromCustomerIdAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetAddressesFromCustomer.Command { CustomerId = customerId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{customerId:Guid}/address/available")]
    [ProducesResponseType(typeof(ApiResponse<Address>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableAddressFromCustomerIdAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetAddressesByAvailable.Command { CustomerId = customerId, Available = true });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{customerId:Guid}/address/unavailable")]
    [ProducesResponseType(typeof(ApiResponse<Address>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnavailableAddressFromCustomerIdAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetAddressesByAvailable.Command { CustomerId = customerId, Available = false });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Address>),StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] Address address)
    {
        var mediator = await Mediator.Send(new CreateAddress.Command { Address = address });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Address>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Address address)
    {
        var mediator = await Mediator.Send(new UpdateAddress.Command { Address = address });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

}
