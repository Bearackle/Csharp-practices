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
    public class GetProductQuery : IRequest<IEnumerable<Product>>
    {
        public GetProductQuery() { }
        public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<Product>>
        {
            private readonly IDbContext _context;
            public GetProductQueryHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetProductQuery query, CancellationToken cancellationToken)
            {
                var data = await (from p in _context.Products
                           select p).ToListAsync();
                return data;
            }
        }
    }
}