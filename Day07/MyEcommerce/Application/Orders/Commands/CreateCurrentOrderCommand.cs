using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using MyEcommerce.Domain.Enums;
using MyEcommerce.Domain.Interfaces;
using MyEcommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Orders
{
    public class CreateCurrentOrderCommand : IRequest<int>
    {
        public IEnumerable<CartItem> _CartItems { get; set; }
        public OrderViewModel _OrderViewModel { get; set; }
        public CreateCurrentOrderCommand(IEnumerable<CartItem> CartItems, OrderViewModel OrderViewModel) {
            _CartItems = CartItems;
            _OrderViewModel = OrderViewModel;
        }
        public class CreateCurrentOrderHandler : IRequestHandler<CreateCurrentOrderCommand, int>
        {
            private readonly IDbContext _context;
            private readonly ICurrentUser _currentUser;
            public CreateCurrentOrderHandler(IDbContext context, ICurrentUser user)
            {
                _context = context;
                _currentUser = user;
            }
            public async Task<int> Handle(CreateCurrentOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = new Order()
                {
                    CustomerName = request._OrderViewModel.CustomerName,
                    Address = request._OrderViewModel.Address,
                    UserId = _currentUser.UserId,
                    Status = (int) OrderStatus.PENDING
                };
                order.Items = new List<OrderItem>();
                foreach(var item in request._CartItems)
                {
                    order.Items.Add(new OrderItem()
                    {
                        Product = item.Product,
                        Quantity = item.Quantity,
                        ProductPrice = item.Product.Price
                    });
                }
                _context.Orders.Add(order);
                return await _context.SaveChangesAsync();
            }
        }
    }
}