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
    public class GetProductFilterQuery : IRequest<IEnumerable<Product>>
    { 
        public string Key { get; set; }
        public GetProductFilterQuery(string key) {
            this.Key = key;
        }
        public class GetProductFilterHandler : IRequestHandler<GetProductFilterQuery, IEnumerable<Product>>
        {
            private readonly IDbContext _context;
            public GetProductFilterHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetProductFilterQuery request, CancellationToken cancellationToken)
            {
                string key = "\"" + request.Key + "\"";
                var products = await _context.Products
                        .SqlQuery("SELECT * FROM Products WHERE CONTAINS((ProductName),@p0)", key)
                        .ToListAsync();          
                return products;
            }
        }
    }
}