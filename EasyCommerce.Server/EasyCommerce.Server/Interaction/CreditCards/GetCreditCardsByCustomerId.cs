using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CreditCards;

public class GetCreditCardsByCustomerId
{
    public class Command : IRequest<ApiResponse<IEnumerable<CreditCard>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<CreditCard>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);

        public async Task<ApiResponse<IEnumerable<CreditCard>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<IEnumerable<CreditCard>>();
            var result = await _applicationDbContext
                                    .CreditCards
                                    .Where(x => x.CustomerId == request.CustomerId)
                                    .ToListAsync();

            return apiResponse.Ok(result, nameof(Category));
        }
    }
}
