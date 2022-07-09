using EasyCommerce.Server.Interaction.Categories;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter<Category>))]
public class CategoryController : ApiController<Category>
{
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var mediator = await Mediator.Send(new GetCategories.Command());
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("products/available")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListWithAvailableProductsAsync()
    {
        var mediator = await Mediator.Send(new GetCategoriesByProductAvailableStatus.Command { Available = true });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("products/unavailable")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListWithUnavailableProductsAsync()
    {
        var mediator = await Mediator.Send(new GetCategoriesByProductAvailableStatus.Command { Available = false });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("available")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByAvailableAsync()
    {
        var mediator = await Mediator.Send(new GetCategoriesByAvailable.Command { Available = true });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("unavailable")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListByUnavailableAsync()
    {
        var mediator = await Mediator.Send(new GetCategoriesByAvailable.Command { Available = false });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        var mediator = await Mediator.Send(new GetCategoryFromId.Command { Id = id });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{title}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] string title)
    {
        var mediator = await Mediator.Send(new GetCategoryFromTitle.Command { Title = title });
        return StatusCode(mediator.ResponseStatusCode, mediator);

    }

    [HttpGet("{parentId:Guid}/sub/categories")]
    [ProducesResponseType(typeof(ApiResponse<CategoryEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSubCategoriesFromCategoryIdAsync([FromRoute] Guid parentId)
    {
        var mediator = await Mediator.Send(new GetChildCategoriesFromParentCategoryId.Command { CategoryId = parentId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("{parentId:Guid}/sub/{childId:Guid}/categories")]
    [ProducesResponseType(typeof(ApiResponse<CategoryEntity>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChildCategoriesFromChildCategoryIdAsync([FromRoute] Guid childId)
    {
        var mediator = await Mediator.Send(new GetChildCategoriesFromChildCategoryId.Command { ParentId = childId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Category>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] Category category)
    {
        var mediator = await Mediator.Send(new CreateCategory.Command { Category = category });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Category>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] Category category)
    {
        var mediator = await Mediator.Send(new UpdateCategory.Command { Category = category });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
