using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Common.TokenManager;
using EasyCommerce.Server.Shared.Domain.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Login;

public class LoginUser
{
    public class Command : IRequest<ApiResponse<LoginRequest>>
    {
        public LoginRequest LoginRequest { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<LoginRequest>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IJwtAuthenticationManager jwtAuthenticationManager, IMapper mapper)
            => (_applicationDbContext, _jwtAuthenticationManager, _mapper) = (applicationDbContext, jwtAuthenticationManager, mapper);
        public async Task<ApiResponse<LoginRequest>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<LoginRequest>();
            if (request.LoginRequest.Email.IsWhiteSpaceOrEmpty() || request.LoginRequest.Password.IsWhiteSpaceOrEmpty()) return apiResponse.BadRequest(request.LoginRequest);

            var userEntity = await _applicationDbContext
                                    .Users
                                    .FirstOrDefaultAsync(x => x.Email == request.LoginRequest.Email 
                                                         && x.Password == request.LoginRequest.Password, cancellationToken);

            if (userEntity.IsNull()) return apiResponse.NotFound(request.LoginRequest);

            request.LoginRequest.Token = _jwtAuthenticationManager.BuildToken();
            
            return apiResponse.Ok(request.LoginRequest);
        }
    }
}
