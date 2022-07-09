using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Addresses;

public class UpdateAddress
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

            var result = await _applicationDbContext
                                .Address
                                .FirstOrDefaultAsync(x => x.Id == request.Address.Id, cancellationToken);

            if (result.IsNull()) return apiResponse.NotFound(request.Address);

            var addressEntity = _mapper.Map(request.Address, result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if(save == 0) return null;
            return apiResponse.NoContent();
        }
    }
}
