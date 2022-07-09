using EasyCommerce.Server.Interaction.Searchs;
using EasyCommerce.Server.Shared.Common;
using EasyCommerce.Server.Shared.Common.Filters;
using EasyCommerce.Server.Shared.Domain.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[TypeFilter(typeof(ApiExceptionCatcherFilter))]
public class SearchController : ApiController
{
    [HttpGet("search-by-categories-and-products/{searchTerm}")]
    [ProducesResponseType(typeof(ApiResponse<SearchByCategoriesAndProductsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync([FromRoute] string searchTerm)
    {
        var mediator = await Mediator.Send(new SearchTermByCategoriesAndProducts.Command { SearchTerm = searchTerm });
        return StatusCode(mediator.ResponseStatusCode, mediator);
    }
}
