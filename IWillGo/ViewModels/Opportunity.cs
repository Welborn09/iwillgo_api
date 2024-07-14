using IWillGo.Model;
using Newtonsoft.Json;

namespace IWillGo.ViewModels
{
    public class Opportunity : ViewModelBase
    {

        public string EventID { get; set; }

        public string EventName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string EventDate {  get; set; }

        public string EventTimeFrom { get; set; }

        public string EventTimeTo { get; set; }

        public string Description { get; set; }

        public Member HostId { get; set; }

        public string Active { get; set; }

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
            model.HostId = this.HostId.ToModel();
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
            ret.HostId = new Member().FromModel(model.HostId);
            ret.Active = model.Active;
            ret.MemberCount = model.MemberCount;
            return ret;
        }

    }
}
