namespace MyEcommerce.Migrations.Application
{
    using MyEcommerce.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyEcommerce.Infrastructure.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Application";
        }

        protected override void Seed(MyEcommerce.Infrastructure.AppDbContext context)
        {
           //List<Category> categories = new List<Category>();
           // categories.AddRange( new List<Category>() {
           //     new Category() {
           //             CategoryName = "Food",
           //     },
           //     new Category()
           //     {
           //         CategoryName = "Bevarage"
           //     },
           //     new Category()
           //     {
           //         CategoryName = "Clothing"
           //     } }
           // );
           // context.Categories.AddRange( categories );
           // context.SaveChanges();
        }
    }
}
