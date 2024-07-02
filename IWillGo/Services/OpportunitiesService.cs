using System;
using System.Collections;
using System.Linq;
using IWillGo.ViewModels;

namespace IWillGo.Services
{
    public interface IOpportunitiesService
    {
        IEnumerable<string> GetOpenOpportunities();
    }

    public class OpportunitiesService : IOpportunitiesService {

        public OpportunitiesService() { }

        public IEnumerable<string> GetOpenOpportunities()
        {
            var opps = new List<string>
            {
                "Community Clean Up",
                "Hope for Lunch",
                "Clothing Gifting"
            };

            //TODO: Grab Events from the db - this will populate a grid or calendar

            return opps;
        }
    }
}
