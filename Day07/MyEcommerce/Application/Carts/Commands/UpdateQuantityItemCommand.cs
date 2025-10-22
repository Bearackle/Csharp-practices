using MediatR;
using MyEcommerce.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Carts.Commands
{
    public class UpdateQuantityItemCommand : IRequest<int>
    {
        public int CartItemId { get; set; }
        public int ItemQuantity { get; set; }
        public UpdateQuantityItemCommand(int cartItemId, int itemQuantity)
        {
            CartItemId = cartItemId;
            ItemQuantity = itemQuantity;
        }
        public class UpdateQuantityItemHandler : IRequestHandler<UpdateQuantityItemCommand, int>
        {
            private readonly IDbContext _context;
            public UpdateQuantityItemHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateQuantityItemCommand request, CancellationToken cancellationToken)
            {
                var item = await _context.CartItems.FindAsync(request.CartItemId);
                if(item != null)
                {
                    item.Quantity = request.ItemQuantity;
                    await _context.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
        }
    }
}