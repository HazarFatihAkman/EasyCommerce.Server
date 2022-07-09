using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Categories;

public class CreateCategory
{
    public class Command : IRequest<ApiResponse<Category>>
    {
        public Category Category { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Category>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper) => (_applicationDbContext, _mapper) = (applicationDbContext, mapper);

        public async Task<ApiResponse<Category>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Category>();
            if (request.Category.IsNull()) return apiResponse.BadRequest(request.Category);

            var categoryEntity = _mapper.Map<CategoryEntity>(request.Category);
            await _applicationDbContext.Categories.AddAsync(categoryEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            var category = await _applicationDbContext
                                .Categories
                                .FirstOrDefaultAsync(x => x.Title == request.Category.Title, cancellationToken);

            return apiResponse.Created(category);
        }
    }
}
