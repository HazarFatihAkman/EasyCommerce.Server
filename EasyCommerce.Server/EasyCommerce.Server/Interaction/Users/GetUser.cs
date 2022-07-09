using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Users;

public class GetUser
{
    public class Command : IRequest<ApiResponse<User>>
    {
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<User>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);
        public async Task<ApiResponse<User>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<User>();
            if (request.UserId.GuidEmpty()) return apiResponse.BadRequestForGet(request.UserId);

            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            if(user.IsNull()) return apiResponse.NotFoundForGet(request.UserId);

            return apiResponse.Ok(user);
        }
    }
}
