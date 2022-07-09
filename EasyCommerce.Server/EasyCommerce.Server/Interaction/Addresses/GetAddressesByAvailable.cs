using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Addresses;

public class GetAddressesByAvailable
{
    public class Command : IRequest<ApiResponse<IEnumerable<AddressEntity>>>
    {
        public Guid CustomerId { get; set; }
        public bool Available { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<AddressEntity>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<IEnumerable<AddressEntity>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<IEnumerable<AddressEntity>>();
            if (request.CustomerId.GuidEmpty()) return apiResponse.BadRequestForGet(request.CustomerId, nameof(Address));
            var customerEntity = await _applicationDbContext
                                    .Customers
                                    .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);
            if (customerEntity.IsNull()) return apiResponse.NotFoundForGet(request.CustomerId, nameof(Address));

            var result = await _applicationDbContext
                        .Address
                        .Include(x => x.Customer)
                        .Where(x => x.Customer == customerEntity && x.Available == request.Available)
                        .ToListAsync(cancellationToken);

            return apiResponse.OkForGet(result, nameof(Address));
        }
    }
}
