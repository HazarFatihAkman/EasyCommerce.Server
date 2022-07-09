using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Users;

public class CreateUser
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
            if (request.User.IsNull()) return apiResponse.BadRequest(request.User);

            var userEntity = _mapper.Map<UserEntity>(request.User);
            await _applicationDbContext.Users.AddAsync(userEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();
            var user = await _applicationDbContext
                                .Users
                                .FirstOrDefaultAsync(x => x.Email == request.User.Email && x.Password == request.User.Password, cancellationToken);
            return apiResponse.Created(user);
        } 
    }
}
