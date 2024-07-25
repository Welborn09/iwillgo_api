using IWillGo.ViewModels;
using System.Collections.Specialized;

namespace IWillGo.Services.Interfaces
{
    public interface IMemberService
    {
        Task<ApiGetListResponse<Member>> ListAsync(NameValueCollection searchOptions);
        Task<Member> GetAsync(string id);
        Task<Member> SaveAsync(RegisterMember client);
        Task<Member> ValidateUser(string email, string password);
        Task<int> GetMemberCount(string eventId);
    }
}
