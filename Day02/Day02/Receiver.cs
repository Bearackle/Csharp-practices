using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    internal class Receiver
    {
        public void Subscribe(Publisher p)
        {
            p.evt += News;
        }
        public void News(object sender ,object data)
        {
            Console.WriteLine("News Receive: " + data.ToString());
        }
    }
}
