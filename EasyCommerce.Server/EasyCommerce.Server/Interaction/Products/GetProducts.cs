using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Products;

public class GetProducts
{
    public class Command : IRequest<ApiResponse<IEnumerable<Product>>> { }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Product>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<IEnumerable<Product>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var results = await _applicationDbContext
                                .Products
                                .Include(x => x.Category)
                                .Include(x => x.Prices)
                                .ThenInclude(x => x.Tax)
                                .ToListAsync(cancellationToken);
            return new ApiResponse<IEnumerable<Product>>().Ok(results, nameof(Product));
        }
    }
}
