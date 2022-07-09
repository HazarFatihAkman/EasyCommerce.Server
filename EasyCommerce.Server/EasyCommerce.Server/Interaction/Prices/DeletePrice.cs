using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Prices;

public class DeletePrice
{
    public class Command : IRequest<ApiResponse<Price>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Price>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<Price>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Price>();
            if (request.IsNull()) return apiResponse.BadRequestForGet(request.Id, nameof(Price));

            var priceEntity = await _applicationDbContext
                                .Prices
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if(priceEntity.IsNull()) return apiResponse.NotFoundForGet(request.Id, nameof(Price));

            if (priceEntity.IsValidPrice)
            {
                var oldPrice = (await _applicationDbContext
                                .Prices
                                .Where(x => x.ProductId == priceEntity.ProductId)
                                .OrderBy(x => x.CreatedAt.Ticks)
                                .ToListAsync()).First();
                oldPrice.IsValidPrice = true;
            }
            _applicationDbContext.Prices.Remove(priceEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
