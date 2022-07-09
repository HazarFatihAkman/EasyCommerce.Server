using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Customers;

public class UpdateCustomer
{
    public class Command : IRequest<ApiResponse<Customer>>
    {
        public Customer Customer { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Customer>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext applicationDbContext, IMapper mapper) => (_applicationDbContext, _mapper) = (applicationDbContext, mapper);
        public async Task<ApiResponse<Customer>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Customer>();
            if (request.Customer.IsNull()) return apiResponse.BadRequest(request.Customer);

            var customerEntity = await _applicationDbContext
                                            .Customers
                                            .FirstOrDefaultAsync(x => x.Id == request.Customer.Id, cancellationToken);
            if (customerEntity.IsNull()) return apiResponse.NotFound(request.Customer);

            _mapper.Map(request.Customer, customerEntity);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();

            return apiResponse.NoContent();
        }
    }
}
