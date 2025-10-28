using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using MyEcommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Orders.Queries
{
    public class GetUserOrdersQuery : IRequest<IEnumerable<Order>>
    {
        public class GetUserOrdersHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<Order>>
        {
            private readonly IDbContext _context;
            private readonly ICurrentUser _currentUser;
            public GetUserOrdersHandler(IDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task<IEnumerable<Order>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
            {
                var orders = await _context.Orders.Where(o => o.UserId.Equals(_currentUser.UserId))
                                .Include("Items")
                             .ToListAsync();
                return orders;
            }
        }
    }
}