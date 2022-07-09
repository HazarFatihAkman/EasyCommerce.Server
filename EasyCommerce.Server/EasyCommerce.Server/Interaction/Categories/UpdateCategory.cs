using AutoMapper;
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

public class UpdateCategory
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

            var categoryEntity = await _applicationDbContext
                                            .Categories
                                            .Include(x => x.Childeren)
                                                .ThenInclude(x => x.Childeren)
                                            .Include(x => x.Childeren)
                                                .ThenInclude(x => x.Products)
                                            .Include(x => x.Products)
                                            .FirstOrDefaultAsync(x => x.Id == request.Category.Id);
            if (categoryEntity.IsNull()) return apiResponse.NotFound(request.Category);

            if (request.Category.Available != categoryEntity.Available)
            {
                SetAvailableCategory(categoryEntity, request.Category.Available);
            }

            _mapper.Map(request.Category, categoryEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }

        public void SetAvailableCategory(CategoryEntity category, bool available)
        {
            category.Available = available;
            if(category.Products.IsNotNull())
            {
                category.Products.ToList().ForEach(x => { x.Available = available; });
            }
            if (category.Childeren.IsNotNull())
            {
                category.Childeren.ToList().ForEach(x =>
                {
                    x.Available = available;
                    if (x.Products.IsNotNull())
                    {
                        x.Products.ToList().ForEach(x => { x.Available = available; });
                    }
                    if (x.Childeren.IsNotNull())
                    {
                        SetAvailableCategory(x, available);
                    }
                });
            }
        }
    }
}
