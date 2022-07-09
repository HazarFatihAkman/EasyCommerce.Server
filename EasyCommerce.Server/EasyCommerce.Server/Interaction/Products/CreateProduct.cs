using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Products;

public class CreateProduct
{
    public class Command : IRequest<ApiResponse<Product>>
    {
        public ProductEntity Product { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<Product>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<Product>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Product>();
            if (request.Product.IsNull()) return apiResponse.BadRequest(request.Product);

            await _applicationDbContext.Products.AddAsync(request.Product, cancellationToken);

            var product = await _applicationDbContext.Products.FirstAsync(x => x.Barcode == request.Product.Barcode, cancellationToken);

            await _applicationDbContext
                    .Prices
                    .AddRangeAsync(request.Product.Prices, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.Created(product, nameof(Product));
        }
    }
}
