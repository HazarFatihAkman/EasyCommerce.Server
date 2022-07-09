using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Settings;

public class CreateSetting
{
    public class Command : IRequest<ApiResponse<Setting>>
    {
        public Setting Setting { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Setting>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Setting>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Setting>();
            if (request.Setting.IsNull()) return apiResponse.BadRequest(request.Setting);
            var settingEntity = _mapper.Map<SettingEntity>(request.Setting);

            await _applicationDbContext.Settings.AddAsync(settingEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();
            return apiResponse.Created(settingEntity, nameof(Setting));

        }
    }
}
