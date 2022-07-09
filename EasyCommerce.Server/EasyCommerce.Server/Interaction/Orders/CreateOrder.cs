using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Orders;

public class CreateOrder
{
    public class Command : IRequest<ApiResponse<Order>>
    {
        public Order Order { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<Order>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Order>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Order>();

            if (request.Order.IsNull()) return apiResponse.BadRequest(request.Order);

            var mappedOrderEntity = _mapper.Map<OrderEntity>(request.Order);
            await _applicationDbContext.Orders.AddAsync(mappedOrderEntity);
            var save = await _applicationDbContext.SaveChangesAsync();
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.Created(mappedOrderEntity, nameof(Order));
        }
    }
}
