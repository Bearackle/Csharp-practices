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

namespace MyEcommerce.Application.Carts.Queries
{
    public class GetCartItemsQuery : IRequest<IEnumerable<CartItem>>
    {
        public class GetCartItemHandler : IRequestHandler<GetCartItemsQuery, IEnumerable<CartItem>>
        {
            private readonly IDbContext _context;
            private readonly ICurrentUser _currentUser;
            public GetCartItemHandler(IDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<IEnumerable<CartItem>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
            {
                var items = await _context.Carts.Where(c => c.UserId.Equals(_currentUser.UserId))
                            .SelectMany(c => c.CartItem)
                            .Include(ci => ci.Product)
                            .ToListAsync();
                return items;
            }
        }
    }
}