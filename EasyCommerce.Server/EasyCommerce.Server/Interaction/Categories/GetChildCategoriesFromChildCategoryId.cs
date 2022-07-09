using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Categories;

public class GetChildCategoriesFromChildCategoryId
{
    public class Command : IRequest<ApiResponse<IEnumerable<CategoryEntity>>>
    {
        public Guid ParentId { get; set; }
    }

    public class Hamdler : IRequestHandler<Command, ApiResponse<IEnumerable<CategoryEntity>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Hamdler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<IEnumerable<CategoryEntity>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<IEnumerable<CategoryEntity>>();
            if (request.ParentId.GuidEmpty()) return apiResponse.BadRequestForGet(request.ParentId, nameof(Category));
            var result = await _applicationDbContext
                                        .Categories
                                        .Include(x => x.Parent)
                                        .Include(x => x.Childeren)
                                        .Include(x => x.Products)
                                            .ThenInclude(x => x.Prices)
                                                .ThenInclude(x => x.Tax)
                                        .Where(x => x.ParentId == request.ParentId)
                                        .ToListAsync();
            return apiResponse.OkForGet(result, nameof(Category));
        }
    }
}
