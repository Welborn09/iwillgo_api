using System.Collections.Specialized;
using IWillGo.Services.Interfaces;
using IWillGo.ViewModels;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Search.SearchOptions;

namespace IWillGo.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo memberRepo;
        public MemberService(IMemberRepo _getRepo)
        {
            this.memberRepo = _getRepo;
        }

        #region Get Members
        public Task<Member> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiGetListResponse<Member>> ListAsync(NameValueCollection searchOptions)
        {
            var response = new ApiGetListResponse<Member>();
            var options = new MemberSearchOptions(searchOptions);
            var memberResult = await memberRepo.GetAsync(options);
            var returnMembers = new List<Member>();

            memberResult.items.ToList().ForEach(x =>
            {
                returnMembers.Add(new Member().FromModel(x));
            });

            response.Items = returnMembers;
            response.Count = memberResult.totalCount;

            return response;
        }
        #endregion

        #region Save Member
        public async Task<Member> SaveAsync(Member client)
        {
            //Validate we have valid info

            //validate we don't already have an email existing
            var existingClient = await memberRepo.GetAsync(new MemberSearchOptions() { Email = client.Email});
            if (existingClient.totalCount > 0 && (client.Id == null || existingClient.items.First().Id != client.Id))
                throw new ClientExistsException("Client already exists");

            var model = client.ToModel();

            await memberRepo.SaveAsync(model);
            return new Member().FromModel(model);

        }
        #endregion


        #region Exceptions
        public class ClientExistsException : SystemException
        {
            public ClientExistsException(string message) : base(message)
            {

            }
        }
        #endregion
    }
}
