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
    public class GetPopularCategoryItemsQuery : IRequest<IEnumerable<Product>>
    {
        public int _CategoryId { get; set; }
        public GetPopularCategoryItemsQuery(int CategoryId)
        {
           
            _CategoryId = CategoryId;  
        }
        public class GetPopularCategoryItemsHandler : IRequestHandler<GetPopularCategoryItemsQuery, IEnumerable<Product>>
        {
            private readonly IDbContext _context;
            public GetPopularCategoryItemsHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Product>> Handle(GetPopularCategoryItemsQuery request, CancellationToken cancellationToken)
            {
                var pproduct = await _context.Products
                                .Where(p => p.CategoryId == request._CategoryId)
                                .OrderBy(p => p.Price)
                                .Include(p => p.Images)
                                .Take(5)
                                .ToListAsync();
                return pproduct;
            }
        }   
    }
}