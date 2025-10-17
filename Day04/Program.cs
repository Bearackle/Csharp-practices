using Day04.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /*EF Core là framework (thư viện khung) để ánh xạ các đơn vị dữ liệu mô tả bằng lớp (đối tượng) vào cơ sở dữ liệu quan hệ,
             * nó cho phép ánh xạ vào các bảng CSDL, tạo CSDL, truy vấn với LINQ, tạo và cập nhật vào database.
             */
            //tạo database nếu không tồn tại
            Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
            using (var context = new AppDbContext())
            {
                //insert product
                context.products.Add(new Product { Name = "Sams1", Price = 100, Provider = "SSVN", BrandId = 1});
                context.SaveChanges();
                //insert nhiều đối tượng
                context.products.AddRange(new Product[]
                {
                    new Product(){Name = "Sams2", Price = 101, Provider="SSVN", BrandId =1},
                    new Product(){Name = "Sams3", Price = 102, Provider="SSVN" ,BrandId =1},
                    new Product(){Name = "Sams4", Price = 103, Provider="SSVN" ,BrandId =1},
                    new Product(){Name = "Sams5", Price = 104, Provider="SSVN" ,BrandId =1},
                    new Product(){Name = "Sams6", Price = 105, Provider="SSVN" ,BrandId =1},
                });
                // có thể dùng async để lợi dụng tính năng bất đồng bộ
                await context.SaveChangesAsync();
                //đọc dữ liệu
                var products = await context.products.ToListAsync();
                products.ForEach(p => Console.WriteLine($"{p.Name} {p.Price}"));
                Console.WriteLine();
                //Sử dụng linq để truy vấn bảng product
                Console.WriteLine("----Select low price product");
                var productsLowPrice = await (from p in context.products
                                              where p.Price == 101
                                              select p).ToListAsync();
                productsLowPrice.ForEach(p => Console.WriteLine($"{p.Name} {p.Price}"));
                // update data
                var productSams6 = await (from p in context.products
                                          where p.ProductId == 6
                                          select p).FirstOrDefaultAsync();
                if(productSams6 != null)
                {
                    productSams6.Name = "Sams7";
                    productSams6.Price = 130;
                    Console.WriteLine("Updating product sams7");
                    await context.SaveChangesAsync();
                }
                // hoặc có thể không cần track đối tượng mà vẫn update được, nhưng vẫn phải đảm bảo validation
                context.Entry(context.products.FirstOrDefault(p => p.ProductId == 12)).State = EntityState.Detached;
                var updateProduct = new Product()
                {
                    ProductId = 12,
                    Name = "Sams6",
                    Price = 220
                };
                context.products.Attach(updateProduct);
                context.Entry(updateProduct).Property(p => p.Price).IsModified = true;
                context.SaveChanges();

                //relationship
                // one to one or zero
                Console.WriteLine(context.students.First().Address.Address1);
                //one to many
                Console.WriteLine(context.students.Find(4).Grade.GradeName);
                context.students.Remove(context.students.Find(4));
                context.SaveChanges(); // loi 
            }
        }
    }
}
