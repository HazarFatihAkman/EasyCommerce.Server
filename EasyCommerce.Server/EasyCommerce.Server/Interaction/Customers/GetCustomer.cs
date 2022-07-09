using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Customers;

public class GetCustomer
{
    public class Command : IRequest<ApiResponse<Customer>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, ApiResponse<Customer>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);
        public async Task<ApiResponse<Customer>> Handle(Command request, CancellationToken cancellationToken)
        {
            var apiResponse = new ApiResponse<Customer>();
            if (request.Id.GuidEmpty()) return apiResponse.BadRequestForGet(request.Id, nameof(Customer));
            var customer = await _applicationDbContext
                                    .Customers
                                    .Include(x => x.Addresses)
                                    .Include(x => x.Orders)
                                    .Include(x => x.CreditCards)
                                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if(customer.IsNull()) return apiResponse.NotFoundForGet(request.Id, nameof(Customer));
            return apiResponse.OkForGet(customer, nameof(Customer));
        }
    }
}
