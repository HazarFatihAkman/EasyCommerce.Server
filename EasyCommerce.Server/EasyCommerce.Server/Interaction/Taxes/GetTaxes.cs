using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Taxes;

public class GetTaxes
{
    public class Command : IRequest<ApiResponse<IEnumerable<Tax>>>
    {

    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Tax>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ApiResponse<IEnumerable<Tax>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext
                                .Taxes
                                .ToListAsync(cancellationToken);
            return new ApiResponse<IEnumerable<Tax>>().Ok(result, nameof(Tax));
        }
    }
}
