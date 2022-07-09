using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Settings;

public class GetSettings
{
    public class Command : IRequest<ApiResponse<IEnumerable<Setting>>> { }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Setting>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<IEnumerable<Setting>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var result = await _applicationDbContext.Settings.ToListAsync();
            return new ApiResponse<IEnumerable<Setting>>().OkForGet(result, nameof(Setting));
        }
    }
}
