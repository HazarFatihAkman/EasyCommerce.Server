using EasyCommerce.Server.Shared;
using EasyCommerce.Server.Shared.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyCommerce.Server.Interaction.Customers;

public class GetCustomers
{
    public class Command : IRequest<ApiResponse<IEnumerable<Customer>>>
    {

    }

    public class Handler : IRequestHandler<Command, ApiResponse<IEnumerable<Customer>>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Handler(IApplicationDbContext applicationDbContext) => (_applicationDbContext) = (applicationDbContext);
        public async Task<ApiResponse<IEnumerable<Customer>>> Handle(Command request, CancellationToken cancellationToken)
        {
            var customers = await _applicationDbContext
                                    .Customers
                                    .Include(x => x.Addresses)
                                    .Include(x => x.Orders)
                                    .Include(x => x.CreditCards)
                                    .ToListAsync(cancellationToken);
            return new ApiResponse<IEnumerable<Customer>>().Ok(customers, nameof(Customer));
        }
    }
}
