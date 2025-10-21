using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public long Price { get; set; }
        public int ProductStock { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductAttribute> Attributes { get; set; }
        public CreateProductCommand(Product product)
        {
            this.ProductName = product.ProductName;
            this.Price = product.Price;
            this.ProductStock = product.ProductStock;
            this.CategoryId = product.CategoryId;
            this.Images = product.Images;
            this.Attributes = product.Attributes;
        }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IDbContext _context;
            private readonly IMediator _mediator;
            public CreateProductCommandHandler(IDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;   
            }
            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                Product product = new Product()
                {
                    ProductName = request.ProductName,
                    Price = request.Price,
                    ProductStock = request.ProductStock,
                    CategoryId = request.CategoryId,
                    Images = request.Images,
                    Attributes = request.Attributes
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);
                return product.ProductId;
            }
        }
    }
}