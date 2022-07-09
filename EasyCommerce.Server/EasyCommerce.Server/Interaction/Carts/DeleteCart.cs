using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Carts;

public class DeleteCart
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
            if (request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id);

            var result = await _applicationDbContext
                                            .Carts
                                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if(result.IsNull()) return apiResponse.NotFoundForGet(result.Id);

            _applicationDbContext.Carts.Remove(result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();
            return apiResponse.NoContent();
        }
    }
}
