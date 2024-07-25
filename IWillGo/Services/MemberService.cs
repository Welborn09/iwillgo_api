using System.Collections.Specialized;
using IWillGo.Services.Interfaces;
using IWillGo.ViewModels;
using IWillGo.DataAccess.Interfaces;
using IWillGo.Search.SearchOptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;

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
        public async Task<Member> ValidateUser(string email, string password)
        {
            var m = await getRepo.Login(email, password);
            return new Member().FromModel(m);
        }
        #endregion

        #region Get Members

        public async Task<Member> GetAsync(string id)
        {
            var models = getRepo.GetAsync(id).Result;
            var m = models.FirstOrDefault();
            if (m == null) { m = new Model.Member() { Id = id }; }
            return new Member().FromModel(m);
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
        public async Task<Member> SaveAsync(RegisterMember member)
        {
            //Validate we have valid info
            Member _member = new Member();
            //validate we don't already have an email existing
            var existingMember = await getRepo.GetAsync(new MemberSearchOptions() { Email = member.Email});
            if (existingMember.totalCount > 0 && (member.Email == null || existingMember.items.First().Email != member.Email))
                throw new MemberExistsException("Member already exists");

            _member.FirstName = member.FirstName;
            _member.LastName = member.LastName;
            _member.Email = member.Email;
            _member.Password = member.Password;

            var model = _member.ToModel();

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
