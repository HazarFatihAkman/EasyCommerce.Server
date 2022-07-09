using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Products;

public class GetProductsFromCategoryId
{
    public class Command : IRequest<ApiResponse<IEnumerable<ProductEntity>>>
    {
        public Guid CategoryId { get; set; }
        public bool Available { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<ProductEntity>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<IEnumerable<ProductEntity>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<IEnumerable<ProductEntity>>();
            if (request.CategoryId.GuidEmpty()) return apiResponse.BadRequestForGet(request.CategoryId, nameof(Product));
            var result = await _applicationDbContext
                        .Products
                        .Where(x => x.CategoryId == request.CategoryId && x.Available == request.Available)
                        .ToListAsync(cancellationToken);

            return apiResponse.OkForGet(result, nameof(Product));
        }
    }
}
