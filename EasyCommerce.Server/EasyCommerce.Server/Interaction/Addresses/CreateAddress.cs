using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Addresses;

public class CreateAddress
{
    public class Command : IRequest<ApiResponse<Address>>
    {
        public Address Address { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Address>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper) => (_applicationDbContext, _mapper) = (applicationDbContext, mapper);
        public async Task<ApiResponse<Address>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Address>();
            if (request.Address.IsNull()) return apiResponse.BadRequest(request.Address);
            var addressEntity = _mapper.Map<AddressEntity>(request.Address);

            await _applicationDbContext.Address.AddAsync(addressEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            var response = await _applicationDbContext
                                            .Address
                                            .Include(x => x.Customer)
                                            .FirstOrDefaultAsync(x => x.AddressName == request.Address.AddressName && x.CustomerId == request.Address.CustomerId, cancellationToken);

            return apiResponse.Created(response, nameof(Address));
        }
    }
}
