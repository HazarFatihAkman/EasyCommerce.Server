using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CargoCompanies;

public class GetCargoCompany
{
    public class Command : IRequest<ApiResponse<CargoCompany>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<CargoCompany>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<CargoCompany>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CargoCompany>();
            if (request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id);

            var result = await _applicationDbContext
                                            .CargoCompanies
                                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (result.IsNull()) return apiResponse.NotFoundForGet(request.Id);

            return apiResponse.OkForGet(result, nameof(CargoCompany));
        }
    }
}
