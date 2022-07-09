using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Users;

public class GetUsers
{
    public class Command : IRequest<ApiResponse<IEnumerable<User>>> {}

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<User>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);
        public async Task<ApiResponse<IEnumerable<User>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userList = await _applicationDbContext.Users.ToListAsync();
            return new ApiResponse<IEnumerable<User>>().OkForGet(userList, nameof(User));
        }
    }
}
