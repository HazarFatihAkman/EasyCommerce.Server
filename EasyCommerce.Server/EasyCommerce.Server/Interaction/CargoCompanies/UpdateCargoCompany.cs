using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CargoCompanies;

public class UpdateCargoCompany
{
    public class Command : IRequest<ApiResponse<CargoCompany>>
    {
        public CargoCompany CargoCompany { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<CargoCompany>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<CargoCompany>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CargoCompany>();
            if (request.CargoCompany.IsNull()) return apiResponse.BadRequest(request.CargoCompany);

            var result = await _applicationDbContext
                                            .CargoCompanies
                                            .FirstOrDefaultAsync(x => x.Id == request.CargoCompany.Id, cancellationToken);
            if (result.IsNull()) return apiResponse.NotFound(request.CargoCompany);

            _mapper.Map(request.CargoCompany, result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
