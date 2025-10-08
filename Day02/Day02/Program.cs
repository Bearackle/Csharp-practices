using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*Generic type:
             * Generic là kiểu đại diện, nó cho phép tạo mã nguồn code không phụ thuộc vào kiểu dữ liệu cụ thể, 
             * chỉ khi code thực thi thì kiểu cụ thể mới xác định
             * Xây dựng phương thức swap
             */
            int a = 1;
            int b = 2;
            Console.WriteLine($"Before swap: a = {a}, b = {b}");
            Swap<int>(ref a, ref b);
            Console.WriteLine($"After swap: a = {a}, b = {b}");
            /*
             * Hoặc có thể xây dựng một class generic như bên dưới
             */
            MyContracts<int> ctr = new MyContracts<int>();
            ctr.setValue(a);
            Console.WriteLine($"Value contract {ctr.getValue()}");
            /* Sorted List
                Nếu tập dữ liệu của bạn được sắp xếp dựa trên một key (khóa), thì bạn có thể dùng đến SortedList<Tkey,TValue>. 
                Lớp này sắp xếp dữ liệu dựa trên một key, kiểu đề làm key là bất kỳ.
             */
            var products = new SortedList<string, string>();
            products.Add("Samsung", "SS S7");
            products.Add("Aaptop", "Asus ROG");
            products["Iphone"] = "ip 17";
            foreach (var i in products)
            {
                Console.WriteLine(i);
            }
            /*Queue: Hàng đợi là mô hình FIFO (first in, first out - vào trước, ra trước hay đến trước được phục vụ trước), 
                nó giải quyết các bài toán thực tế giống mua hàng...
             */
            Queue<string> sanpham_ordered = new Queue<string>();
            sanpham_ordered.Enqueue("IP11");
            sanpham_ordered.Enqueue("IP12");
            sanpham_ordered.Enqueue("IP13");
            sanpham_ordered.Enqueue("IP14");

            while (sanpham_ordered.Count > 0)
            {
                var or = sanpham_ordered.Dequeue();
                Console.WriteLine($"Xử lý {or}, còn lại {sanpham_ordered.Count}");
            }
            /*
                Ngăn xếp stack khá giống hàng đợi, nhưng khác đó là LIFO (last in, first out) - vào sau thì ra trước,
                nó giống như xếp hàng hóa vào các container, vào nhà kho - cái nào đưa vào sau thì khi thảo dỡ lại thực hiện đầu tiên,
                nó giống như xếp đĩa vào cọc đĩa CD cái nào đưa vào cọc trước sẽ được lấy ra sau .
             */
            Stack<string> diacd = new Stack<string>();
            diacd.Push("spiderman");
            diacd.Push("ironman");
            diacd.Push("superman");
            while (diacd.Count > 0)
            {
                var item = diacd.Pop();
                Console.WriteLine($"Lấy đĩa: {item} còn lại {item.Count()}");
            }

            /*LinkedList: Trong thư viện .NET cung cấp lớp LinkedList<T> là loại danh sách liên kết kép.
                Danh sách liên kết là một danh sách chứa các phần tử, mỗi phần tử trong loại danh sách này được gọi là một nút (Node).
            Mỗi nút ngoài dữ liệu của nút đó,
                nó sẽ gồm hai biến - một biến tham chiếu đến Node phía trước, một nút tham chiếu đến nút phía sau.
            */
            LinkedList<string> courses = new LinkedList<string>();
            var firstNode = courses.AddFirst("Distribute database");
            courses.AddLast("Big Data");
            courses.AddAfter(firstNode, "Web development");
            Console.WriteLine(courses.First);
            Console.WriteLine($"Contains Big Data? {courses.Contains("Big Data")}");
            Console.WriteLine($"Find Introduction to Programing: {courses.Find("Introduction to Programing")?.Value}");

            /*
                Lớp Dictionary<Tkey,TValue> khá giống SortedList, Dictionary được thiết kế với mục đích tăng hiệu quả với tập dữ liệu lớn, phức tạp.
                Một đối tượng dữ liệu lưu vào Dictionary dưới dạng cặp key/value, truy cập đến phần tử thông qua key hoặc thông qua vị trí (index) của dữ liệu trong danh sách.
                Chú ý, không cho phép trùng key.
             */
            Dictionary<string, int> sodem = new Dictionary<string, int>();
            sodem["one"] = 1;
            sodem["two"] = 2;
            sodem["three"] = 3;
            sodem["four"] = 4;
            foreach (KeyValuePair<string, int> item in sodem)
            {
                Console.WriteLine($"spell: {item.Key} value {item.Value}");
            }
            /*
            Deletegate: Delegate (hàm ủy quyền) là một kiểu dữ liệu, nó dùng để tham chiếu (trỏ đến) đến các hàm (phương thức) có tham số và kiểu trả về phù hợp với khai báo kiểu.
            Khi dùng đến delegate bạn có thể gán vào nó một, nhiều hàm (phương thức) có sự tương thích về tham số, kiểu trả về, sau đó dùng nó để gọi hàm (giống con trỏ trong C++), 
            các event trong C# chính là các hàm được gọi thông qua delegate
            Lưu ý: để delegate có thể trỏ đến method, method đó phải có signature giống với delegate, có kiểu trả về giống nhau và có số lượng tham số, kiểu dữ liệu tham số giống nhau
                   Delegate cũng có thể trỏ đến nhiều method
            */
            Logger log = Info;
            log("This is notification");
            log = Warning;
            log("This is warning");
            /*
            Action: là delegate đặc biệt có kiểu trả về là void, có thể nhận 0 hoặc nhiều tham số, 
            có sẵn trong namespace System
             */
            Action<string> showLog = Info;
            showLog("Action OK");

            /*
             Event: là cơ chế để một đối tượng (đối tượng của lớp) này thông báo đến đối tượng khác có 
             điều gì đó mà lớp khác quan tâm vừa xảy ra. Lớp mà từ đó gửi đi sự kiện gọi tên nó là publisher và các lớp nhận được sự kiện gọi là là các subsriber.
             */
            Publisher publisher = new Publisher();
            Receiver receiver = new Receiver();
            receiver.Subscribe(publisher);
            // không thể: publisher.evt = null để loại bỏ các subcribers từ bên ngoài
            publisher.Send("New message for you");
           
        }
        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        public delegate void Logger(string message);
        static public void Info(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Format("Info: {0}", s));
            Console.ResetColor();
        }
        static public void Warning(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("Waring: {0}", s));
            Console.ResetColor();
        }
    }
    class MyContracts<T>
    {
        private T _value;
        public T getValue()
        {
            return _value;
        }
        public void setValue(T value)
        {
            _value = value;
        }
    }
}
