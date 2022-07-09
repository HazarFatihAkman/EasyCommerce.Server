using EasyCommerce.Server.Interaction.Customers;
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
[TypeFilter(typeof(ApiExceptionCatcherFilter<Customer>))]
public class CustomerController : ApiController<Customer>
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Customer>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync()
    {
        var mediator = await Mediator.Send(new GetCustomers.Command());
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{customerId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Customer>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid customerId)
    {
        var mediator = await Mediator.Send(new GetCustomer.Command { Id = customerId});
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Customer>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] Customer customer)
    {
        var mediator = await Mediator.Send(new CreateCustomer.Command { Customer = customer });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Customer>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Customer customer)
    {
        var mediator = await Mediator.Send(new UpdateCustomer.Command { Customer = customer });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
