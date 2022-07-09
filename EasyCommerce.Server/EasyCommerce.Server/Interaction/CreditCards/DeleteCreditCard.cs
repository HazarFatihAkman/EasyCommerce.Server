using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CreditCards;

public class DeleteCreditCard
{
    public class Command : IRequest<ApiResponse<CreditCard>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<CreditCard>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public Handler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<CreditCard>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CreditCard>();
            var result = await _applicationDbContext
                                .CreditCards
                                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result.IsNull()) return apiResponse.NotFoundForGet(request.Id);

            _applicationDbContext.CreditCards.Remove(result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();
            return apiResponse.NoContentForGet(request.Id);
        }
    }
}
