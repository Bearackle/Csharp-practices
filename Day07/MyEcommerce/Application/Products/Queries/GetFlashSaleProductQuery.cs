using MediatR;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Application.Products.Queries
{
    public class GetFlashSaleProductQuery : IRequest<IEnumerable<Product>>
    {
        public GetFlashSaleProductQuery() { }
        public class GetFlashSaleQueryHandler : IRequestHandler<GetFlashSaleProductQuery, IEnumerable<Product>>
        {
            private readonly IDbContext _context;
            public GetFlashSaleQueryHandler(IDbContext context) {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetFlashSaleProductQuery request, CancellationToken cancellationToken)
            {
                var FlsProducts = await _context.Products
                    .OrderBy(p => p.Price).Take(5)
                    .ToListAsync();
                return FlsProducts;
            }
        }
    }
}