using AutoMapper;
using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using EasyCommerce.Server.Shared.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Customers;

public class CreateCustomer
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

            var customerEntity = _mapper.Map<CustomerEntity>(request.Customer);

            await _applicationDbContext.Customers.AddAsync(customerEntity, cancellationToken);

            var save = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (save == 0) return apiResponse.InternalServerError();
            return apiResponse.Created(customerEntity, nameof(Customer));
        }
    }
}
