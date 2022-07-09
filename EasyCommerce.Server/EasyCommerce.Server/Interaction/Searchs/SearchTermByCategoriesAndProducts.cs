using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Searchs;

public class SearchTermByCategoriesAndProducts
{
    public class Command : IRequest<ApiResponse<SearchByCategoriesAndProductsResponse>>
    {
        public string SearchTerm { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<SearchByCategoriesAndProductsResponse>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<SearchByCategoriesAndProductsResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var categories = _mapper.Map<List<Category>>(await _applicationDbContext
                                    .Categories
                                    .Where(x => x.Title.ToLower().Contains(request.SearchTerm.ToLower(CultureInfo.InvariantCulture))
                                             || x.Description.ToLower().Contains(request.SearchTerm.ToLower(CultureInfo.InvariantCulture))
                                             || x.KeyWords.ToLower().Contains(request.SearchTerm.ToLower(CultureInfo.InvariantCulture)))
                                    .ToListAsync(cancellationToken));
            var products = _mapper.Map<List<Product>>(await _applicationDbContext
                                    .Products
                                    .Where(x => x.Title!.ToLower().Contains(request.SearchTerm.ToLower(CultureInfo.InvariantCulture))
                                             || x.KeyWords!.ToLower().Contains(request.SearchTerm.ToLower(CultureInfo.InvariantCulture))
                                             || x.Description!.ToLower().Contains(request.SearchTerm.ToLower(CultureInfo.InvariantCulture)))
                                    .ToListAsync(cancellationToken));
            return new ApiResponse<SearchByCategoriesAndProductsResponse>().OkForGet(new SearchByCategoriesAndProductsResponse
            {
                Categories = categories,
                Products = products
            }, nameof(SearchByCategoriesAndProductsResponse));
        }
    }
}
