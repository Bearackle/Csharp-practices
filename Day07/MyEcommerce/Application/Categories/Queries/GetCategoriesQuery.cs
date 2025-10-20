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

namespace MyEcommerce.Application.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public GetCategoriesQuery() { }
        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery,IEnumerable<Category>>
        {
            private readonly IDbContext _context;
            public GetCategoriesQueryHandler(IDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery query, CancellationToken token)
            {
                var categories = await (from c in _context.Categories
                                  select c).ToListAsync();
                return categories;
            }
        }
    }
}