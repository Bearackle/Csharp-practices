using Microsoft.AspNet.Identity.EntityFramework;
using MyEcommerce.Application.Interfaces;
using MyEcommerce.Domain.Entities;
using MyEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyEcommerce.Infrastructure
{
    public class AppDbContext : DbContext, IDbContext
    {
        public DbSet<Product> Products {  get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        public AppDbContext() : base("AppConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DbContext>());
        }

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}