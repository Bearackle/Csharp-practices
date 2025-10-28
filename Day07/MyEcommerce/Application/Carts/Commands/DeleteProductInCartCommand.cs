using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Carts.Commands
{
    public class DeleteProductInCartCommand : IRequest<Unit>
    {
        public int CartItemId {  get; set; }
        public DeleteProductInCartCommand(int CartItemId)
        {
            this.CartItemId = CartItemId;
        }
        public class DeleteProdcutInCartHandler : IRequestHandler<DeleteProductInCartCommand, Unit>
        {
            private readonly IDbContext _context;
            private readonly ICurrentUser _currentUser;
            public DeleteProdcutInCartHandler(IDbContext context, ICurrentUser currentUser) 
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<Unit> Handle(DeleteProductInCartCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.CartItems.FindAsync(request.CartItemId);
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}