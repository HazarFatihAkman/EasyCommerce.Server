using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Products;

public class GetProduct
{
    public class Command : IRequest<ApiResponse<ProductEntity>>
    {
        public Guid ProductId { get; set; } 
    }

    public class Handler : IRequestHandler<Command, ApiResponse<ProductEntity>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<ProductEntity>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<ProductEntity>();
            if (request.ProductId.GuidEmpty()) return apiResponse.NoContentForGet(request.ProductId);
            var result = await _applicationDbContext
                        .Products
                        .Include(x => x.Category)
                        .Include(x => x.Prices)
                            .ThenInclude(x => x.Tax)
                        .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
            if (result.IsNull()) return apiResponse.NotFoundForGet(request.ProductId);
            return apiResponse.Ok(result);
        }
    }
}
