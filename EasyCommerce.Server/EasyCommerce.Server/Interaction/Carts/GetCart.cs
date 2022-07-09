using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Carts;

public class GetCart
{
    public class Command : IRequest<ApiResponse<Cart>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Cart>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<Cart>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Cart>();
            if(request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id);

            var result = await _applicationDbContext
                                            .Carts
                                            .Include(x => x.Product)
                                                .ThenInclude(x => x.Prices)
                                                    .ThenInclude(x => x.Tax)
                                            .Include(x => x.Order)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (result.IsNull()) return apiResponse.NotFoundForGet(request.Id);

            return apiResponse.OkForGet(result, nameof(Cart));
        }
    }
}
