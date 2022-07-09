using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Common.TokenManager;
using EasyCommerce.Server.Shared.Domain.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Login;

public class LoginCustomer 
{
    public class Command : IRequest<ApiResponse<LoginRequest>>
    {
        public LoginRequest LoginRequest { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<LoginRequest>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public Handler(IApplicationDbContext applicationDbContext, IJwtAuthenticationManager jwtAuthenticationManager)
            => (_applicationDbContext, _jwtAuthenticationManager) = (applicationDbContext, jwtAuthenticationManager);
        public async Task<ApiResponse<LoginRequest>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<LoginRequest>();
            if (request.LoginRequest.Email.IsWhiteSpaceOrEmpty() || request.LoginRequest.Password.IsWhiteSpaceOrEmpty()) return apiResponse.BadRequest(request.LoginRequest);

            var customerEntity = await _applicationDbContext
                                    .Customers
                                    .FirstOrDefaultAsync(x => x.Email == request.LoginRequest.Email
                                                         && x.Password == request.LoginRequest.Password, cancellationToken);

            if (customerEntity.IsNull()) return apiResponse.NotFound(request.LoginRequest);

            request.LoginRequest.Token = _jwtAuthenticationManager.BuildToken();

            return apiResponse.Ok(request.LoginRequest);
        }
    }
}
