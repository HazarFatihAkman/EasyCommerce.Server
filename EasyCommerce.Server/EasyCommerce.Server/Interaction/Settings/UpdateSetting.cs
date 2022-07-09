using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Settings;

public class UpdateSetting
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

            var settingEntity = await _applicationDbContext
                                        .Settings
                                        .FirstOrDefaultAsync(x => x.PrefixKey == request.Setting.PrefixKey 
                                                               && x.Id == request.Setting.Id, cancellationToken);
            if (settingEntity.IsNull()) return apiResponse.NotFound(request.Setting);

            _mapper.Map(request.Setting, settingEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
