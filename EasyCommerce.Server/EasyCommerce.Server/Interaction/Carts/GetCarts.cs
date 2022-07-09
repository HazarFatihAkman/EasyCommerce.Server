using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Carts;

public class GetCarts
{
    public class Command : IRequest<ApiResponse<IEnumerable<Cart>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Cart>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<IEnumerable<Cart>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<IEnumerable<Cart>>();
            if (request.CustomerId.GuidEmpty()) return apiResponse.BadRequestForGet(request.CustomerId, nameof(Cart));

            var result = await _applicationDbContext
                                        .Carts
                                        .Include(x => x.Product)
                                        .Include(x => x.Order)
                                        .Where(x => x.CustomerId == request.CustomerId)
                                        .ToListAsync();
            if (result.Count() == 0) return apiResponse.NotFoundForGet(request.CustomerId, nameof(Cart));

            return apiResponse.OkForGet(result, nameof(Cart));
        }
    }
}

