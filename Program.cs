
/*Array trong C#
    Mảng là một cấu trúc dữ liệu được sử dụng dụng để lưu trữ một tập dữ liệu cùng kiểu
*/
Console.WriteLine("Array - List - dictionary");
int[] bienMang = new int[5] { 1, 2, 3, 4, 6 };

Console.WriteLine($"len: {bienMang.Length}");
Console.WriteLine($"rank: {bienMang.Rank}");

int[] bienClone = (int[])bienMang.Clone(); // shallow copy

Console.WriteLine($"value at index [1]: {bienMang.GetValue(1)}");
Console.WriteLine($"value min: {bienClone.Min()}");
Console.WriteLine($"value max: {bienClone.Max()}");
Console.WriteLine($"value sum: {bienClone.Sum()}");
Console.WriteLine($"Array Binary Search: {Array.BinarySearch(bienClone, 1)}");

Console.WriteLine("CopyTo Array");

int[] copy = new int[5];
bienClone.CopyTo(copy, 0);

Array.ForEach(copy, n => Console.Write($"{n} "));

int[] fill = new int[5];
Array.Fill(fill, 1);

Array.ForEach(fill, n => Console.Write($"{n} "));

Console.WriteLine($"\nDo 3 exists: {Array.Exists<int>(fill, n => n == 3)}");
Console.WriteLine($"Where is 3: {Array.Find<int>(fill, n => n == 3)}");
Console.WriteLine($"Where Find Index of 3: {Array.FindIndex<int>(bienClone, n => n == 1000)}");
int[] findall = Array.FindAll<int>(fill, p => p % 2 == 0);
Console.WriteLine($"Where is find all 1");
Array.ForEach(findall, n => Console.Write($"{n} "));

int[] sort_v = new int[] { 6, 5, 4, 3, 2, 1 };
Array.Sort(sort_v);
Array.ForEach(sort_v, i => Console.Write($"{i} "));
Console.WriteLine();



// ------------ Collection trong c# ----------------
/*
    Một collection (bộ, tập hợp) là một nhóm các đối tượng có sự liên quan đến nhau.
    Số đối tượng trong collect có thể thay đổi tăng giảm. 
    Có nhiều loại collection, chúng được tập hợp vào namespace System.Collections. 
    Thường thì một lớp collection có các phương thức để thêm, bớt, lấy tổng phần tử.
*/

IEnumerable<int> cls = new int[] { 1, 2, 3, 4, 5 };

foreach (int i in cls)
{
    Console.Write($"{i} ");
}
Console.WriteLine();

IEnumerator<int> it = cls.GetEnumerator();
while (it.MoveNext())
{
    int item = it.Current;
    Console.Write($"{item} ");
}
Console.WriteLine();

/*
    ICollection: Giao diện này được triển khai bở các generic collection. 
    Với nó lấy tổng phần tử bằng thuộc tính Count, copy các phần tử vào mảng bằng CopyTo, 
    thêm bớt phần tử với Add, Remove, Clear.

         IEnumerable<T>
               ↑
         ICollection<T>
               ↑
    IList<T>, ISet<T>, IDictionary<TKey,TValue>
*/

/* IList: Giao diện này kế thừa ICollection<T> là một danh sách các phần tử truy cập được theo vị trí của nó. 
    Nó có indexer, phương thức để chèn phần tử xóa phần tử Insert RemoveAt.
*/
List<int> number = new List<int>();
number.Add(1);
number.AddRange([2, 3]);
number.Add(5);
number.Insert(3, 4);

number.RemoveAt(number.Count - 1);
number.Remove(2); // remove item có value = input
number.RemoveRange(2, 2);
number.Clear();
number.ForEach(i => Console.Write($"{i} "));
number.RemoveAll(n => n % 2 == 0);


/* Searching trong List*/
List<int> nums = new List<int>() { 1, 2, 3, 4, 5 };

Console.WriteLine($"{nums.IndexOf(1)}");
Console.WriteLine($"{nums.LastIndexOf(5)}");
Console.WriteLine($"{nums.FindIndex(i => i % 2 == 0)}");
Console.WriteLine($"{nums.FindLastIndex(i => i % 2 == 0)}");
Console.WriteLine($"{nums.Find(i => i > 4)}"); // find first index of item that match predicate
List<int> findAll = nums.FindAll(i => i > 3);

findAll.ForEach(i => Console.Write($"{i} "));

