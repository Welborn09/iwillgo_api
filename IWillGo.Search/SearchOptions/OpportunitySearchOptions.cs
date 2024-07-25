using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.Search.SearchOptions
{
    public class OpportunitySearchOptions : BaseSearchOptions
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string EventId { get; set; }
        public string MemberId { get; set; }
        public string HostId { get; set; }
        public DateTime? EventDateFrom { get; set; }
        public DateTime? EventDateTo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public OpportunitySearchOptions(NameValueCollection options) : base(options)
        { }

        public OpportunitySearchOptions() { }

        protected override dynamic AddSqlParameters()
        {
            dynamic val = new ExpandoObject();
            val.EventId = EventId;
            val.HostId = HostId;
            val.MemberId = MemberId;
            val.EventDateFrom = EventDateFrom;
            val.EventDateTo = EventDateFrom;
            val.City = City;
            val.State = State;
            val.Zip = Zip;
            val.Offset = PageNumber != null ? (PageNumber - 1) * PageSize : 0;
            val.PageSize = PageSize != null ? PageSize : 9999999;
            return val;
        }

        protected override void LoadOptions(NameValueCollection options)
        {
            PageNumber = options.AllKeys.Contains("PageNumber") ? Convert.ToInt32(options["PageNumber"]) : null;
            PageSize = options.AllKeys.Contains("PageSize") ? Convert.ToInt32(options["PageSize"]) : null;
            EventId = options.AllKeys.Contains("PK_Opportunity") ? options["PK_Opportunity"] : null;
            HostId = options.AllKeys.Contains("Host_UserId") ? options["Host_UserId"] : null;
            MemberId = options.AllKeys.Contains("MemberId") ? options["MemberId"] : null;
            EventDateFrom = options.AllKeys.Contains("EventDateFrom") ? options["EventDateFrom"].TryParseToNullableDateTime() : null;
            EventDateTo = options.AllKeys.Contains("EventDateTo") ? options["EventDateTo"].TryParseToNullableDateTime() : null;
            City = options.AllKeys.Contains("City") ? options["City"] : null;
            State = options.AllKeys.Contains("State") ? options["State"] : null;
            Zip = options.AllKeys.Contains("Zip") ? options["Zip"] : null;

        }
    }
}
