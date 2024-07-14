using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Search.SearchOptions;
using IWillGo.Services.Interfaces;
using IWillGo.ViewModels;

namespace IWillGo.Services
{
    public interface IOpportunitiesService
    {
        Task<ApiGetListResponse<Opportunity>> GetOpenOpportunities(NameValueCollection searchOptions);
        Task<Opportunity> GetOpportunity(string eventId);
    }

    public class OpportunitiesService : IOpportunitiesService 
    {
        private readonly IGetOpportunityRepo getRepo;

        private readonly IMemberService memberService;

        public OpportunitiesService(IGetOpportunityRepo _getRepo, IMemberService _memberService) 
        {
            this.getRepo = _getRepo;
            this.memberService = _memberService;
        }

        public async Task<ApiGetListResponse<Opportunity>> GetOpenOpportunities(NameValueCollection searchOptions)
        {
            var response = new ApiGetListResponse<Opportunity>();
            var options = new OpportunitySearchOptions(searchOptions);
            var oppResult = await getRepo.GetAsync(options);
            var returnOpportunity = new List<Opportunity>();

            oppResult.items.ToList().ForEach(async x =>
            {
                var host = await memberService.GetAsync(x.HostId.Id);
                x.HostId = host.ToModel();
                returnOpportunity.Add(new Opportunity().FromModel(x));
            });

            returnOpportunity.Sort((a,b) => b.EventDate.CompareTo(a.EventDate));

            response.Items = returnOpportunity;
            response.Count = oppResult.totalCount;

            return response;
        }

        public async Task<Opportunity> GetOpportunity(string eventId)
        {
            var model = await getRepo.GetById(eventId);
            var host = await memberService.GetAsync(model.HostId.Id);
            model.HostId = host.ToModel();
            return new Opportunity().FromModel(model);
        }
    }
}
