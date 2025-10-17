using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Day03
{
    public class WikipediaDownloader
    {
        public static async Task download(string title)
        {
            string url = $"https://en.wikipedia.org/w/api.php?action=query&prop=extracts&format=json&titles={Uri.EscapeDataString(title)}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# App (https://example.com)");
                string json = await client.GetStringAsync(url);
                Console.WriteLine($"Finish download {title}");
                string fileName = $"{title.Replace(" ", "_")}.json";
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    await writer.WriteAsync(json);
                    Console.WriteLine($"Finish write {title} file");
                }
            }
        }
    }
}
