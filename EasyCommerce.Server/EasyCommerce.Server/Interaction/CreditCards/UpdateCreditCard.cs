using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CreditCards;

public class UpdateCreditCard 
{
    public class Command : IRequest<ApiResponse<CreditCard>>
    {
        public CreditCard CreditCard { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<CreditCard>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApiResponse<CreditCard>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CreditCard>();
            if (request.CreditCard.IsNull()) return apiResponse.BadRequest(request.CreditCard);

            var result = await _applicationDbContext
                                            .Carts
                                            .FirstOrDefaultAsync(x => x.Id == request.CreditCard.Id, cancellationToken);
            if (result.IsNull()) return apiResponse.NotFound(request.CreditCard);

            _mapper.Map(request.CreditCard, result);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
