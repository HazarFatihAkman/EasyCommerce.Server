using EasyCommerce.Server.Interaction.Settings;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SettingController : ApiController<Setting>
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Setting>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var meditor = await Mediator.Send(new GetSettings.Command { });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }
    [HttpGet("{prefixKey}")]
    [ProducesResponseType(typeof(ApiResponse<Setting>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] string prefixKey)
    {
        var meditor = await Mediator.Send(new GetSettingByPrefixKey.Command { PrefixKey = prefixKey });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }

    [HttpPut("create")]
    [ProducesResponseType(typeof(ApiResponse<Setting>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] Setting setting)
    {
        var meditor = await Mediator.Send(new CreateSetting.Command { Setting = setting });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Setting>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Setting setting)
    {
        var meditor = await Mediator.Send(new UpdateSetting.Command { Setting = setting });
        return StatusCode(meditor.ResponseStatusCode, meditor);
    }
}
