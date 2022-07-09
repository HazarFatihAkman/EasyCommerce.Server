using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Users;

public class UpdateUser
{
    public class Command : IRequest<ApiResponse<User>>
    {
        public User User { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<User>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper) => (_applicationDbContext, _mapper) = (applicationDbContext, mapper);
        public async Task<ApiResponse<User>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<User>();
            var userEntity = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.User.Id, cancellationToken);
            if(userEntity.IsNull()) return apiResponse.NotFound(request.User);

            _mapper.Map(request.User, userEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();
            return apiResponse.NoContent();
            
        }
    }
}
