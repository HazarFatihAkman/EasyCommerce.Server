using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Carts;

public class UpdateCart
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

            var result = await _applicationDbContext
                                            .Carts
                                            .FirstOrDefaultAsync(x => x.Id == request.Cart.Id, cancellationToken);
            if (result.IsNull()) return apiResponse.NotFound(request.Cart);

            _mapper.Map(request.Cart, result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
