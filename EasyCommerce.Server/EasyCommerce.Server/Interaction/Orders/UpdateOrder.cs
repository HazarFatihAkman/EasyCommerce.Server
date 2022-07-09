using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Orders;

public class UpdateOrder
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

            var orderEntity = await _applicationDbContext
                                        .Orders
                                        .FirstOrDefaultAsync(x => x.Id == request.Order.Id, cancellationToken);    
            if(orderEntity.IsNull()) return apiResponse.NotFound(request.Order);

            _mapper.Map(request.Order, orderEntity);
            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
