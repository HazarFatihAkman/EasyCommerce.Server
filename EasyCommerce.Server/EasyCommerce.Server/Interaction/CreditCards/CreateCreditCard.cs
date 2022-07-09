using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.CreditCards;

public class CreateCreditCard
{
    public class Command : IRequest<ApiResponse<CreditCard>>
    {
        public CreditCard CreditCard { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<CreditCard>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CreditCard>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<CreditCard>();
            if(request.CreditCard.IsNull()) return apiResponse.BadRequest(request.CreditCard);

            var creditCardEntity = _mapper.Map<CreditCardEntity>(request.CreditCard);
            await _applicationDbContext.CreditCards.AddAsync(creditCardEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.Created(request.CreditCard);
        }
    }
}
