using EasyCommerce.Server.Interaction.Login;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[TypeFilter(typeof(ApiExceptionCatcherFilter<LoginRequest>))]
public class LoginController : ApiController<LoginRequest>
{
    [AllowAnonymous]
    [HttpPost("user")]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
    {
        var mediator = await Mediator.Send(new LoginUser.Command { LoginRequest = loginRequest });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [AllowAnonymous]
    [HttpPost("customer")]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>),StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginCustomerAsync([FromBody] LoginRequest loginRequest)
    {
        var mediator = await Mediator.Send(new LoginCustomer.Command { LoginRequest = loginRequest });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

}
