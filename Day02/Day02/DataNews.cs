using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    internal class DataNews : EventArgs
    {
        private object _data;
        public DataNews(object data)
        {
            _data = data;
        }
        public object GetData()
        {
            return _data;
        }
        public void SetData(string data)
        {
            _data = data;
        }
       
    }
}
