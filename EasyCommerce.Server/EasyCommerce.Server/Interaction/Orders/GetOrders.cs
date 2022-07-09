using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Orders;

public class GetOrders
{
    public class Command : IRequest<ApiResponse<IEnumerable<Order>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Order>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ApiResponse<IEnumerable<Order>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext
                                .Orders
                                .Include(x => x.Address)
                                .Include(x => x.Customer)
                                .Include(x => x.Carts)
                                .Include(x => x.CargoCompany)
                                .Where(x => x.CustomerId == request.CustomerId)
                                .ToListAsync(cancellationToken);
            return new ApiResponse<IEnumerable<Order>>().OkForGet(result, nameof(Order));
        }
    }
}
