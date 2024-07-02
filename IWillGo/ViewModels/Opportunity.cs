using IWillGo.Model;
namespace IWillGo.ViewModels
{
    public class Opportunity : ViewModelBase
    {

        public string EventID { get; set; }

        public string EventName { get; set; }

        public string Address { get; set; }

        public string EventDate {  get; set; }


        public Model.Opportunity ToModel()
        {
            var model = new Model.Opportunity();
            model.EventName = this.EventName;            
            model.Address = this.Address;
            model.EventDate = this.EventDate;
            return model;

        }

        public Opportunity FromModel(Model.Opportunity model)
        {
            var ret = new Opportunity();
            ret.EventName = model.EventName;
            ret.Address = model.Address;
            ret.EventDate = model.EventDate;
            return ret;
        }

    }
}
