using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Settings;

public class GetSettingByPrefixKey
{
    public class Command : IRequest<ApiResponse<Setting>>
    {
        public string PrefixKey { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Setting>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<Setting>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiRespose = new ApiResponse<Setting>();
            if (request.PrefixKey.IsNullOrEmpty() || request.PrefixKey.IsNullOrWhiteSpace()) return apiRespose.BadRequestForGet(request.PrefixKey);

            var setting = await _applicationDbContext.Settings.FirstOrDefaultAsync(x => x.PrefixKey == request.PrefixKey, cancellationToken);
            if (setting.IsNull()) return apiRespose.NotFoundForGet(request.PrefixKey);

            return apiRespose.OkForGet(setting, nameof(Setting));
        }
    }
}
