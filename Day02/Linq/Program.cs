using Day02;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>() {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty Bigin"},
            };
            var products = new List<Product>()
            {
                new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };

            /*
             * Mệnh đề from in xác định nguồn dữ liệu cho câu truy vấn, nguồn dữ liệu phải là các đối tượng triển khai
             * interface IEnumerable.
             * Mệnh đề select chỉ ra các dữ liệu (field) được lấy ra từ câu truy vấn 
             */
            var selectedProducts = from p in products
                                   select new { p.Name, p.Price };
            foreach(var i in selectedProducts)
            {
                Console.WriteLine(i.Name + " : " + i.Price);
            }
            /* Where: là mệnh đề biểu hiện điều kiện chọn của đối tượng
             */
            var grayProducts = from p in products
                               where p.Colors.Contains("Xám")
                               select new
                               {
                                   p.Name,
                                   p.Price
                               };
            var sanpham = grayProducts.ToList();
            sanpham.ForEach(s => Console.WriteLine(s.ToString()));
            var sanphamMS = products.Where(i => i.Name.Contains("Xám")).ToList();
            /*
             Mệnh đề Order by: sắp xếp thứ tự của các phần tử theo điều kiện*/
            Console.WriteLine("----Order by clause");
            var lowToHighPrice = (from p in products
                                 orderby p.Price
                                 select p.Name).ToList();
            var HightoLowPrice = products.OrderBy(p =>  p.Price).ToList();
            lowToHighPrice.ForEach(s => Console.WriteLine(s));

            var orderByName = (from p in products
                              orderby p.Name descending
                              select p.Name).ToList();
            orderByName.ForEach(s => Console.WriteLine(s));
            var orderByNameMs = products.OrderByDescending(p => p.Name).ToList();

            /*
             * Mệnh đề Group By: Cuối truy vấn có thể sử dụng group thay cho select, 
             * nếu sử dụng group thì nó trả về theo từng nhóm (nhóm lại theo trường dữ liệu nào đó), mỗi phần tử của cấu truy vấn trả về là kiểu IGrouping<TKey,TElement>, 
             * chứa các phần tử thuộc một nhóm.
             */
            Console.WriteLine("-------Group by brand");
            var groupByBrands = (from p in products
                                group p by p.Brand into g
                                select g).ToList(); // có thể bỏ into và select để lấy giá trị trả về mặc định nếu không sử dụng output để xử lý tiếp
            groupByBrands.ForEach(s =>
            {
                Console.WriteLine($"--Brand ID: {s.Key}");
                foreach (var item in s)
                {
                    Console.WriteLine($"{item.Name} : {item.Price}");
                }
            });
            // sử dụng biến trong truy vấn kết hợp với điều kiện tạo group
            var statisticProduct = (from p in products
                                    group p by (
                                        p.Price <= 200 ? "Low price":
                                        p.Price <= 400 ? "Intermediate":
                                        "luxury") into gr
                                        select new {
                                            Segment = gr.Key,
                                            Count = gr.Count(),
                                            AveragePrice = gr.Average(x => x.Price)
                                        }).ToList();
            statisticProduct.ForEach(s => Console.WriteLine(s.ToString()));

            /* Join trong Linq: Thực hiện kết hợp hai nguyền dữ liệu lại với nhau để truy vấn thông tin
             */
            // inner join
            Console.WriteLine("----Inner join");
            var joinBrandAndProduct = (from p in products
                                      join b in brands on p.Brand equals b.ID
                                      select new
                                      {
                                          ProductName = p.Name,
                                          brand = b.Name,
                                          price = p.Price
                                      }).ToList();
            joinBrandAndProduct.ForEach(s => Console.WriteLine(s));
            //left join
            Console.WriteLine("----Left join");
            var leftJoinBrandAndProduct = (from p in products
                                           join b in brands on p.Brand equals b.ID into pg
                                           from b in pg.DefaultIfEmpty()
                                           select new
                                           {
                                               ProductName = p.Name,
                                               brand = b?.Name ?? "No Brand",
                                               price = p.Price
                                           }).ToList();
            leftJoinBrandAndProduct.ForEach(s => Console.WriteLine(s));
            //Any
            var productPriceHigher600 = products.Any(p => p.Price >= 600);
            Console.WriteLine($"Is there any product >= 600: {productPriceHigher600}");
            //All
            var productAllPriceHigherThan200 = products.All(p => p.Price >= 200);
            Console.WriteLine($"Is All Product price >= 200: {productAllPriceHigherThan200}");
            
            ;
        }
    }
}
