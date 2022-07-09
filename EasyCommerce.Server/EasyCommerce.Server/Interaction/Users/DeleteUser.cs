using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Users;

public class DeleteUser
{
    public class Command : IRequest<ApiResponse<User>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<User>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);
        public async Task<ApiResponse<User>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<User>();
            var result = await _applicationDbContext
                                .Users
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result.IsNull()) return apiResponse.NotFoundForGet(request.Id);

            _applicationDbContext.Users.Remove(result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if(save == 0) return apiResponse.InternalServerError();
            return apiResponse.NoContentForGet(request.Id);
        }
    }
}
