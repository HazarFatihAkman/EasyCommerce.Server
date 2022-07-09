using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Taxes;

public class CreateTax
{
    public class Command : IRequest<ApiResponse<Tax>>
    {
        public Tax Tax { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<Tax>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Tax>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Tax>();
            if (request.Tax.IsNull()) return apiResponse.BadRequest(request.Tax);
            var taxEntity = _mapper.Map<TaxEntity>(request.Tax);
            await _applicationDbContext.Taxes.AddAsync(taxEntity, cancellationToken);
            
            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if(save == 0) return apiResponse.InternalServerError();

            return apiResponse.Created(request.Tax);
        }
    }
}
