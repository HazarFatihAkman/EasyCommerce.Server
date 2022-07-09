using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Products;

public class GetProductsByAvailableCase
{
    public class Command : IRequest<ApiResponse<IEnumerable<Product>>>
    {
        public bool Available { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Product>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ApiResponse<IEnumerable<Product>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext
                                .Products
                                .Include(x => x.Category)
                                .Include(x => x.Prices)
                                .ThenInclude(x => x.Tax)
                                .Where(x => x.Available == request.Available)
                                .ToListAsync();
            return new ApiResponse<IEnumerable<Product>>().Ok(result, nameof(Product));
        }
    }
}
