namespace IWillGo.Authentication.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        Task<string> Authenticate(string email, string password);
    }
}
