using EasyCommerce.Server.Interaction.Users;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter<User>))]
public class UserController : ApiController<User>
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<User>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var mediator = await Mediator.Send(new GetUsers.Command());
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{userId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid userId)
    {
        var mediator = await Mediator.Send(new GetUser.Command { UserId = userId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] User user)
    {
        var mediator = await Mediator.Send(new CreateUser.Command { User = user });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] User user)
    {
        var mediator = await Mediator.Send(new UpdateUser.Command { User = user });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpDelete("delete/{userId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid userId)
    {
        var mediator = await Mediator.Send(new DeleteUser.Command { Id = userId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
