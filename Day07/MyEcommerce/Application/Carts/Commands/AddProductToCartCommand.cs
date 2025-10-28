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

namespace MyEcommerce.Application.Carts.Commands
{
    public class AddProductToCartCommand : IRequest<int>
    {
        public Product Product { get; set; }
        public int Quantity {  get; set; }
        public AddProductToCartCommand(Product product, int quantity)
        {
            this.Product = product;
            Quantity = quantity;
        }
        public class AddProductToCartHandler : IRequestHandler<AddProductToCartCommand, int>
        {
            private readonly IDbContext _context;
            private readonly ICurrentUser _currentUser;
            public AddProductToCartHandler(IDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<int> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
            {
                var Cart = _context.Carts.Where(c => c.UserId.Equals(_currentUser.UserId))
                    .Include("CartItem.Product")
                    .FirstOrDefault();
                if (Cart != null && !Cart.CartItem.Any(ci => ci.Product.ProductId.Equals(request.Product.ProductId)))
                {
                    Cart.CartItem.Add(new CartItem()
                    {
                        Product = request.Product,
                        Quantity = request.Quantity,
                    });
                    await _context.SaveChangesAsync();
                    return 1;
                }
                return 0;
            }
        }
    }
}