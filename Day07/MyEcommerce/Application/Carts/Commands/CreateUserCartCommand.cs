using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using MyEcommerce.Domain.Entities;

namespace MyEcommerce.Application.Carts.Commands
{
    public class CreateUserCartCommand : IRequest<int>
    {
        public class CreateUserCartHandler : IRequestHandler<CreateUserCartCommand, int>
        {
            private readonly IDbContext _context;
            private readonly ICurrentUser _currentUser;
            public CreateUserCartHandler(IDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<int> Handle(CreateUserCartCommand request, CancellationToken cancellationToken)
            {
                var cart = _context.Carts.Where(c => c.UserId == _currentUser.UserId)
                                .FirstOrDefault();
                if(cart == null && _currentUser.IsAuthenticated)
                {
                    cart = _context.Carts.Add(new Cart()
                    {
                        UserId = _currentUser.UserId,
                    });
                    await _context.SaveChangesAsync();
                }
                return cart.CartId; 
            }
        }
    }
}