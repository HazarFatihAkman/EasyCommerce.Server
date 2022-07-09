using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Categories;

public class GetCategories
{
    public class Command : IRequest<ApiResponse<IEnumerable<CategoryEntity>>>
    {
    }
    
    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<CategoryEntity>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<IEnumerable<CategoryEntity>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<IEnumerable<CategoryEntity>>();
            var result = (await _applicationDbContext
                                .Categories
                                .Include(x => x.Parent)
                                .Include(x => x.Products)
                                    .ThenInclude(x => x.Prices)
                                        .ThenInclude(x => x.Tax)
                                .Include(x => x.Childeren)
                                .ToListAsync(cancellationToken)).Where(x => x.ParentId.IsNull()).ToList();
            
            return apiResponse.Ok(result, nameof(Category));
        }
    }
}
