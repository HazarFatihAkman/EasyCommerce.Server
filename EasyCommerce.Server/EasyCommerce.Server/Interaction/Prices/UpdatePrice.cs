using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Prices;

public class UpdatePrice
{
    public class Command : IRequest<ApiResponse<Price>>
    {
        public Guid ProductId { get; set; }
        public Price Price { get; set; }
    }
    public class Handler : IRequestHandler<Command, ApiResponse<Price>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper) 
        {
            _applicationDbContext = applicationDbContext; 
            _mapper = mapper;
        }
        public async Task<ApiResponse<Price>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Price>();
            if (request.Price.IsNull()) return apiResponse.BadRequest(request.Price);

            var priceEntity = await _applicationDbContext
                                    .Prices
                                    .FirstOrDefaultAsync(x => x.Id == request.Price.Id, cancellationToken);
            if (priceEntity.IsNull()) return apiResponse.NotFound(request.Price);

            var currentPrice = await _applicationDbContext
                                  .Prices
                                  .FirstOrDefaultAsync(x => x.ProductId == request.Price.ProductId && x.IsValidPrice == true, cancellationToken);
            if(currentPrice.Id != request.Price.Id && request.Price.IsValidPrice)
            {
                currentPrice.IsValidPrice = false;
            }

            _mapper.Map(request.Price, priceEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
