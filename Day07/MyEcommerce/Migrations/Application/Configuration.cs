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
            //categories.AddRange(new List<Category>() {
            //    new Category() {
            //            CategoryName = "Thời trang & phụ kiện",
            //    },
            //    new Category()
            //    {
            //        CategoryName = "Điện tử & công nghệ"
            //    },
            //    new Category()
            //    {
            //        CategoryName = "Nhà cửa & đời sống"
            //    },
            //    new Category()
            //    {
            //        CategoryName = "Sức khỏe & làm đẹp"
            //    }
            //    ,
            //    new Category()
            //    {
            //        CategoryName = "Trẻ em & đồ chơi"
            //    }
            //    ,new Category()
            //    {
            //        CategoryName = "Thực phẩm & đồ uống"
            //    },
            //    new Category()
            //    {
            //        CategoryName = "Thể thao & ngoài trời"
            //    },
            //    new Category()
            //    {
            //        CategoryName = "Ô tô, xe máy & phụ kiện"
            //    },
            //    new Category()
            //    {
            //        CategoryName = "Hàng thiết yếu & chăm sóc cá nhân"
            //    }
            //    ,
            //    new Category()
            //    {
            //        CategoryName = "Sản phẩm kỹ thuật số / dịch vụ"
            //    }
            //}
            //);
            //context.Categories.AddRange(categories);
            //context.SaveChanges();
        }
    }
}
