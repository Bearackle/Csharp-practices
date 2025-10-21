using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Products.Commands
{
    public class UpdateProductCommad : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public int ProductStock { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductAttribute> Attributes { get; set; }
        public UpdateProductCommad(Product product)
        {
            this.ProductId = product.ProductId;
            this.ProductName = product.ProductName;
            this.Price = product.Price;
            this.ProductStock = product.ProductStock;
            this.CategoryId = product.CategoryId;
            this.Images = product.Images;
            this.Attributes = product.Attributes;
        }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommad,Unit>
        {
            private readonly IDbContext _context;
            public UpdateProductCommandHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateProductCommad request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.ProductId);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {request.ProductId} not found");
                }
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                product.ProductName = request.ProductName;
                product.Price = request.Price;
                product.ProductStock = request.ProductStock;
                product.CategoryId = request.CategoryId;
                if (request.Images != null)
                {
                    foreach (var image in request.Images)
                    {
                        product.Images.Add(image);
                    }
                }
                if (request.Attributes != null)
                {
                    foreach (var attr in request.Attributes)
                    {
                        product.Attributes.Add(attr);
                    }
                }
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}