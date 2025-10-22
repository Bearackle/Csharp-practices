using MyEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyEcommerce.Application.Interfaces
{
    public interface IDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<ProductAttribute> ProductAttributes { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbEntityEntry Entry(object entity);
    }
}
