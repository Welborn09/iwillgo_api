using System.Collections.Specialized;
using IWillGo.Services.Interfaces;
using IWillGo.ViewModels;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Search.SearchOptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IWillGo.Services
{
    public class MemberService : IMemberService
    {
        private readonly IGetMemberRepo getRepo;
        private readonly ISaveMemberRepo saveRepo;
        public MemberService(IGetMemberRepo _getRepo, ISaveMemberRepo _saveRepo)
        {
            this.getRepo = _getRepo;
            this.saveRepo = _saveRepo;
        }

        #region Login
        public async Task<bool> Login(string email, string password)
        {
            return true;
        }
        #endregion

        #region Get Members

        public async Task<Member> GetAsync(string id)
        {
            return new Member().FromModel(await getRepo.GetAsync(id));
        }

        public async Task<ApiGetListResponse<Member>> ListAsync(NameValueCollection searchOptions)
        {
            var response = new ApiGetListResponse<Member>();
            var options = new MemberSearchOptions(searchOptions);
            var memberResult = await getRepo.GetAsync(options);
            var returnMembers = new List<Member>();

            memberResult.items.ToList().ForEach(x =>
            {
                returnMembers.Add(new Member().FromModel(x));
            });

            response.Items = returnMembers;
            response.Count = memberResult.totalCount;

            return response;
        }

        public async Task<int> GetMemberCount(string eventId)
        {
            return await getRepo.GetMemberCount(eventId);
        }
        #endregion

        #region Save Member
        public async Task<Member> SaveAsync(Member member)
        {
            //Validate we have valid info

            //validate we don't already have an email existing
            var existingMember = await getRepo.GetAsync(new MemberSearchOptions() { Email = member.Email});
            if (existingMember.totalCount > 0 && (member.Id == null || existingMember.items.First().Id != member.Id))
                throw new MemberExistsException("Member already exists");

            var model = member.ToModel();

            await saveRepo.SaveAsync(model);
            return new Member().FromModel(model);

        }
        #endregion


        #region Exceptions
        public class MemberExistsException : SystemException
        {
            public MemberExistsException(string message) : base(message)
            {

            }
        }
        #endregion
    }
}
