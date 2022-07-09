using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Categories;

public class GetCategoryFromId
{
    public class Command : IRequest<ApiResponse<CategoryEntity>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<CategoryEntity>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<CategoryEntity>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CategoryEntity>();
            if (request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id);
            var category = await _applicationDbContext
                            .Categories
                            .Include(x => x.Parent)
                            .Include(x => x.Products)
                                .ThenInclude(x => x.Prices)
                                    .ThenInclude(x => x.Tax)
                            .Include(x => x.Childeren)
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (category.IsNull()) return apiResponse.NotFoundForGet(request.Id);
            return apiResponse.OkForGet(category, nameof(Category));
        }
    }
}
