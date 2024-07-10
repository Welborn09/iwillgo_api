using IWillGo.Model;
using Newtonsoft.Json;

namespace IWillGo.ViewModels
{
    public class Opportunity : ViewModelBase
    {

        [JsonProperty("eventId")] 
        public string EventID { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("eventDate")]
        public string EventDate {  get; set; }

        [JsonProperty("eventTimeFrom")]
        public string EventTimeFrom { get; set; }

        [JsonProperty("eventTimeTo")]
        public string EventTimeTo { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hostId")]
        public string HostId { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("memberCount")]
        public int MemberCount { get; set; }


        public Model.Opportunity ToModel()
        {
            var model = new Model.Opportunity();
            model.EventId = EventID;
            model.EventName = this.EventName;            
            model.Address = this.Address;
            model.City = this.City;
            model.State = this.State;
            model.Zip = this.Zip;
            model.EventDate = this.EventDate;
            model.EventTimeFrom = this.EventTimeFrom;
            model.EventTimeTo = this.EventTimeTo;
            model.Description = this.Description;
            model.HostId = this.HostId;
            model.Active = this.Active;
            model.MemberCount = this.MemberCount;
            return model;

        }

        public Opportunity FromModel(Model.Opportunity model)
        {
            var ret = new Opportunity();
            ret.EventID = model.EventId;
            ret.EventName = model.EventName;
            ret.Address = model.Address;
            ret.City = model.City;
            ret.State = model.State;
            ret.Zip = model.Zip;
            ret.EventDate = model.EventDate;
            ret.EventTimeFrom = model.EventTimeFrom;
            ret.EventTimeTo = model.EventTimeTo;
            ret.Description = model.Description;
            ret.HostId = model.HostId;
            ret.Active = model.Active;
            ret.MemberCount = model.MemberCount;
            return ret;
        }

    }
}
