using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Prices;

public class CreatePrice
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

            var currentPrice = await _applicationDbContext
                                  .Prices
                                  .FirstOrDefaultAsync(x => x.ProductId == request.Price.ProductId && x.IsValidPrice == true, cancellationToken);
            if (currentPrice.Id != request.Price.Id && request.Price.IsValidPrice)
            {
                currentPrice.IsValidPrice = false;
            }

            await _applicationDbContext.Prices.AddAsync(_mapper.Map<PriceEntity>(request.Price), cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.Created(request.Price);
        }
    }
}
