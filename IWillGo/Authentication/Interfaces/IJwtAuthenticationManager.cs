using IWillGo.ViewModels;

namespace IWillGo.Authentication.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        Task<AuthenticatedResponse> Authenticate(string email, string password);
        
    }
}
