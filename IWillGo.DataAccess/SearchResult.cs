using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.DataAccess
{
    public class SearchResult
    {
        public SearchResult()
        {
            Ids = new List<String>();
        }
        public List<string> Ids { get; set; }
        public int Count { get; set; }
    }
}
