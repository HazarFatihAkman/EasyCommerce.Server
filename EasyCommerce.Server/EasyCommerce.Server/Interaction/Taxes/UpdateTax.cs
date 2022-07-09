using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Taxes;

public class UpdateTax
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

            var taxEntity = await _applicationDbContext
                                .Taxes
                                .FirstOrDefaultAsync(x => x.Id == request.Tax.Id, cancellationToken);
            if (taxEntity.IsNull()) return apiResponse.NotFound(request.Tax);

            _mapper.Map(request.Tax, taxEntity);
            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
