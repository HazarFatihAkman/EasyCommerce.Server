using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CargoCompanies;

public class CreateCargoCompany
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
            var cargoCompanyEntity = _mapper.Map<CargoCompanyEntity>(request.CargoCompany);
            await _applicationDbContext.CargoCompanies.AddAsync(cargoCompanyEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.Created(request.CargoCompany);

        }
    }
}
