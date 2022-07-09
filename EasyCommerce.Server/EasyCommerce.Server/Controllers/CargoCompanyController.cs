using EasyCommerce.Server.Interaction.CargoCompanies;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter<CargoCompany>))]
public class CargoCompanyController : ApiController<CargoCompany>
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CargoCompany>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var mediator = await Mediator.Send(new GetCargoCompanies.Command { });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("unavailable")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CargoCompany>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByUnavailableAsync()
    {
        var mediator = await Mediator.Send(new GetCargoCompaniesByAvailable.Command { Available = false });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("available")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CargoCompany>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByAvailableAsync()
    {
        var mediator = await Mediator.Send(new GetCargoCompaniesByAvailable.Command { Available = true });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<CargoCompany>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var mediator = await Mediator.Send(new GetCargoCompany.Command { Id = id });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<CargoCompany>), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostAsync([FromBody] CargoCompany cargoCompany)
    {
        var mediator = await Mediator.Send(new CreateCargoCompany.Command { CargoCompany = cargoCompany });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<CargoCompany>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] CargoCompany cargoCompany)
    {
        var mediator = await Mediator.Send(new UpdateCargoCompany.Command { CargoCompany = cargoCompany });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
