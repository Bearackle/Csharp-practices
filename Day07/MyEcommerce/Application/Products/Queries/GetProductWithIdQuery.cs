using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Products.Queries
{
    public class GetProductWithIdQuery : IRequest<Product>
    {
        private int ProductId;
        public GetProductWithIdQuery(int id)
        {
            ProductId = id;
        }
        public class GetProductWithIdHandler : IRequestHandler<GetProductWithIdQuery,Product>
        {
            private readonly IDbContext _context;
            public GetProductWithIdHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<Product> Handle(GetProductWithIdQuery request, CancellationToken cancellation)
            {
                var p = await _context.Products.FindAsync(request.ProductId);
                return p;
            }
        }

    }
}