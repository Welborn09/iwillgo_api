using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.Model
{
    public class Opportunity : BaseModel
    {
        public string EventId { get; set; }

        public string EventName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string EventDate { get; set; }

        public string EventTimeFrom { get; set; }

        public string EventTimeTo { get; set; }

        public string Description { get; set; }

        public Member HostId { get; set; }

        public string Active { get; set; }

        public int MemberCount { get; set; }
    }
}
