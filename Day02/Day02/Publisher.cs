using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    internal class Publisher
    {
        public event EventHandler evt;
        public void Send(object data)
        {
            evt?.Invoke(this, new DataNews(data));
        }
    }
}
