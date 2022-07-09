using EasyCommerce.Server.Interaction.Products;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
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
[TypeFilter(typeof(ApiExceptionCatcherFilter<Product>))]
public class ProductController : ApiController<Product>
{
    [HttpGet("{productId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid productId)
    {
        var mediator = await Mediator.Send(new GetProduct.Command { ProductId = productId });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetListAsync()
    {
        var mediator = await Mediator.Send(new GetProducts.Command { });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("available")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableAsync()
    {
        var mediator = await Mediator.Send(new GetProductsByAvailableCase.Command { Available = true });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("unavailable")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductEntity>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnavailableAsync()
    {
        var mediator = await Mediator.Send(new GetProductsByAvailableCase.Command { Available = false });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpGet("available/category/{categoryId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductEntity>>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableProductsFromCategoryId([FromRoute] Guid categoryId)
    {
        var mediator = await Mediator.Send(new GetProductsFromCategoryId.Command { CategoryId = categoryId, Available = true });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
    [HttpGet("unavailable/category/{categoryId:Guid}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ProductEntity>>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUnavailableProductsFromCategoryId([FromRoute] Guid categoryId)
    {
        var mediator = await Mediator.Send(new GetProductsFromCategoryId.Command { CategoryId = categoryId, Available = false });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync([FromBody] ProductEntity product)
    {
        var mediator = await Mediator.Send(new CreateProduct.Command { Product = product });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ApiResponse<Product>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync([FromBody] ProductEntity product)
    {
        var mediator = await Mediator.Send(new UpdateProduct.Command { Product = product });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
