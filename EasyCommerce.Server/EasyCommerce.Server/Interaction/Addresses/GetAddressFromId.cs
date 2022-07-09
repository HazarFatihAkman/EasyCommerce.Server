using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Addresses;

public class GetAddressFromId
{
    public class Command : IRequest<ApiResponse<AddressEntity>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<AddressEntity>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<AddressEntity>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<AddressEntity>();
            if (request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id, nameof(Address));

            var result = await _applicationDbContext
                                .Address
                                .Include(x => x.Customer)
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if(result.IsNull()) return apiResponse.NotFoundForGet(result.Id, nameof(Address));

            return apiResponse.OkForGet(result, nameof(Address));
        }
    }
}
