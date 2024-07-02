using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.Model
{
    public class Opportunity : BaseModel
    {
        public string EventName { get; set; }

        public string Address { get; set; }

        public string EventDate { get; set; }
    }
}
