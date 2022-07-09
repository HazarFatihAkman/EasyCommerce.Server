using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Products;

public class UpdateProduct
{
    public class Command : IRequest<ApiResponse<Product>>
    {
        public ProductEntity Product { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Product>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper) => (_applicationDbContext, _mapper) = (applicationDbContext, mapper);

        public async Task<ApiResponse<Product>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Product>();
            if (request.Product.IsNull()) return apiResponse.BadRequest(request.Product);

            var productEntity = await _applicationDbContext
                            .Products
                            .FirstOrDefaultAsync(x => x.Id == request.Product.Id, cancellationToken);
            if (productEntity.IsNull()) return apiResponse.NotFound(request.Product);

            var price = request.Product.Prices.FirstOrDefault(x => x.IsValidPrice == true);
            if (productEntity.CurrentPrice.Id != price.Id)
            {
                productEntity.CurrentPrice.IsValidPrice = false;

                await _applicationDbContext
                        .Prices
                        .AddRangeAsync(request.Product.Prices, cancellationToken);
            }

            _mapper.Map(request.Product, productEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if(save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
