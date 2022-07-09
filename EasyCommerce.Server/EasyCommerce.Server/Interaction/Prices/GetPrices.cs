using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Prices;

public class GetPrices
{
    public class Command : IRequest<ApiResponse<IEnumerable<Price>>>
    {
        public Guid ProductId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Price>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ApiResponse<IEnumerable<Price>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext
                                .Prices
                                .Include(x => x.Product)
                                .Include(x => x.Tax)
                                .Where(x => x.ProductId == request.ProductId)
                                .ToListAsync(cancellationToken);
            return new ApiResponse<IEnumerable<Price>>().OkForGet(result, nameof(Price));
        }
    }
}
