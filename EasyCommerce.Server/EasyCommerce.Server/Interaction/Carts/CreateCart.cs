using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Enums;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Carts;

public class CreateCart
{
    public class Command : IRequest<ApiResponse<Cart>>
    {
        public Cart Cart { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<Cart>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<Cart>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Cart>();
            if (request.Cart.IsNull()) return apiResponse.BadRequest(request.Cart);

            if(request.Cart.OrderId.Value.GuidEmpty())
            {
                var order = new Order
                {
                    CustomerId = request.Cart.CustomerId,
                    OrderStatus = OrderStatus.Cart                        
                };
                var orderEntity = _mapper.Map<OrderEntity>(order);
                request.Cart.OrderId = orderEntity.Id;
                await _applicationDbContext.Orders.AddAsync(orderEntity, cancellationToken);
            }
            var cartEntity = _mapper.Map<CartEntity>(request.Cart);
            await _applicationDbContext.Carts.AddAsync(cartEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if(save == 0) return apiResponse.InternalServerError();
            return apiResponse.Created(request.Cart);

        }
    }
}
