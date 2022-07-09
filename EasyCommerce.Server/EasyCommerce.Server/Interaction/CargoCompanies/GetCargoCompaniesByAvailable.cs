using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CargoCompanies;

public class GetCargoCompaniesByAvailable
{
    public class Command : IRequest<ApiResponse<IEnumerable<CargoCompany>>>
    {
        public bool Available { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<CargoCompany>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<IEnumerable<CargoCompany>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var results = await _applicationDbContext
                                .CargoCompanies
                                .Where(x => x.Available == request.Available)
                                .ToListAsync(cancellationToken);

            return new ApiResponse<IEnumerable<CargoCompany>>().Ok(results, nameof(CargoCompany));
        }
    }
}
