using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Orders;

public class GetOrder
{
    public class Command : IRequest<ApiResponse<Order>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Order>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<Order>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Order>();
            if(request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id);

            var orderEntity = await _applicationDbContext
                                    .Orders
                                    .Include(x => x.CargoCompany)
                                    .Include(x => x.Customer)
                                    .Include(x => x.Address)
                                    .Include(x => x.Carts)
                                        .ThenInclude(x => x.Product)
                                            .ThenInclude(x => x.Prices)
                                                .ThenInclude(x => x.Tax)
                                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (orderEntity.IsNull()) return apiResponse.NotFoundForGet(request.Id);

            return apiResponse.OkForGet(orderEntity, nameof(Order));
        }
    }
}
