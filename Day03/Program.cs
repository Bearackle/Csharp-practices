using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Day03
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /*MultiThread trong C#*/
            /*Thread cơ bản:
             * Thread được tạo thông qua lớp Thread, là một luồng chạy chương trình độc lập, khi Thread start, IsAlive = true cho đến khi nó kết thúc.
             * Khi thread kết thúc không thể restart.
             * CLR cấp phát thread memory stack độc lập
             */
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                    Console.Write("A");
            }).Start();
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                    Console.Write("B");
            }).Start();
            //Thread sẽ share data nếu chúng giữ cùng 1 tham chiếu đến biến trong memory stack
            bool isDone = false;
            new Thread(() =>
            {
                if (!isDone)
                {
                    Console.WriteLine("From Thread A");
                    isDone = true;
                }
            }).Start();
            new Thread(() =>
            {
                if (!isDone) {
                    Console.WriteLine("From Thread B");
                    isDone = true;
                }
            }).Start();
            // Có khả năng gây ra xung đột, có thể cả hai thread đều in ra thông điệp => cần thread safety sử dụng exclusive (lock)

            /* Join và Sleep
             * Sử dụng từ khóa Join, đợi thread hoàn tất rồi mới tiếp tục
             * Sleep giữ thread dừng theo thời gian
             * Khi sử dụng Join hoặc sleep, thread bị block và sẽ không tiêu tốn tài nguyên
             */
            Thread t = new Thread(() => { for (int i = 0; i < 10; i++) Console.Write("y"); });
            t.Start();
            t.Join();
            Console.WriteLine("Thread t has ended!");
            // Lưu ý: Thread.Sleep(0) sẽ nhường cpu tạm thời cho các thread khác, Thread.Yield() là phương thức cho nhiệm vụ này
            /*
             * Truyền dữ liệu cho thread
             */
            Thread passData = new Thread(Print);
            passData.Start("Hello World");
            /*
             * Thread Priority:Scheduler của hệ điều hành tham khảo priority để quyết định thread nào được chạy trước khi nhiều thread cùng “ready” chạy.\
             * các mức priority: enum ThreadPriority { Lowest, BelowNormal, Normal, AboveNormal, Highest }
             * Có thể gây thread starvation
             */
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                    Console.Write("C");
            });
            Thread t2 = new Thread(() => {
                for (int i = 0; i < 10; i++)
                Console.Write("D");
             });

            t1.Priority = ThreadPriority.Highest;
            t2.Priority = ThreadPriority.Lowest;

            t1.Start();
            t2.Start();
            /*  
             *  Thread Pool là tập hợp các thread đã được tạo sẵn và tái sử dụng.
             *  */
            ThreadPool.QueueUserWorkItem(
            _ => {
                for (int i = 0; i < 10; i++)
                    Console.Write("E");
            });
            Task.Factory.StartNew(Print, "Y").Wait();
            /*Async: là một cách thức mà khi gọi nó chạy ở chế độ nền (liên quan đến một tiến trình, task), trong khi đó tiến trình gọi nó không bị khóa - block.
            * Trong .NET có triển khai một số mô hình lập trình bất đồng bộ như Asynchronous pattern, mẫu bất đồng bộ theo sự kiện và theo tác vụ (TAP - task-based asynchronous pattern)
            * TAP mô tả cách viết và gọi các hàm async trong C# bằng cách trả về Task thay vì dùng callback hay event.
            */
            
            Task<string> task1 = Async1("A", "B");
            Console.WriteLine(task1.Result); // chờ task1 hoàn thành mới chạy task 2 (blocking)
            Task task2 = Async2();
            /*Sử dụng từ khóa async/await
               Lời gọi hàm Async1 chuyển hướng về chỗ gọi nó khi gặp await (tạm dừng thi hành mã sau await)
               Code trong Async1 phía sau await chỉ được chạy khi task chạy xong
               Khi await hoàn thành thì nó chứa kết quả của Task nếu có
               await chỉ viết được trong hàm có khai báo async
             */
            Task task3 = Async3();
            //Vd download dữ liệu từ wikipedia
            Task download = WikipediaDownloader.download("Albert Einstein");
            await download;

        }
        static void Print(object messageObj)
        {
            string message = (string)messageObj;   
            Console.WriteLine(message);
        }
        public static void WriteLine(string s, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(s);
        }
        public static Task<string> Async1(string thamso1, string thamso2)
        {
            Func<object, string> myfunc = (object thamso) => {
                dynamic ts = thamso;
                for (int i = 1; i <= 10; i++)
                {
                    WriteLine($"{i,5} {Thread.CurrentThread.ManagedThreadId,3} Tham số {ts.x} {ts.y}",
                        ConsoleColor.Green);
                    Thread.Sleep(500);
                }
                return $"Kết thúc Async1! {ts.x}";
            };
            Task<string> task = new Task<string>(myfunc, new { x = thamso1, y = thamso2 });
            task.Start(); 
            return task;
        }
        public static Task Async2()
        {

            Action myaction = () => {
                for (int i = 1; i <= 10; i++)
                {
                    WriteLine($"{i,5} {Thread.CurrentThread.ManagedThreadId,3}",
                        ConsoleColor.Yellow);
                    Thread.Sleep(2000);
                }
            };
            Task task = new Task(myaction);
            task.Start();
            return task;
        }
        public static async Task Async3()
        {
            Action myaction = () => {
                for (int i = 1; i <= 10; i++)
                {
                    WriteLine($"{i,5} {Thread.CurrentThread.ManagedThreadId,3}", ConsoleColor.Yellow);
                }
            };
            Task task = new Task(myaction);
            task.Start();

            await task;
            Console.WriteLine("Async3: Kết thúc");
        }

    }
}
