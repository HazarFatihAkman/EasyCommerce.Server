using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Taxes;

public class GetTax
{
    public class Command : IRequest<ApiResponse<Tax>>
    {
        public Guid Id { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<Tax>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<Tax>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Tax>();
            if (request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id);
            var tax = await _applicationDbContext
                            .Taxes
                            .FirstOrDefaultAsync(cancellationToken);

            if (tax.IsNull()) return apiResponse.NotFoundForGet(request.Id);

            return apiResponse.OkForGet(tax, nameof(Tax));
        }
    }
}
