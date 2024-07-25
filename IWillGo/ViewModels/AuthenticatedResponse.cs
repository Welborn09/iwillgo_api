namespace IWillGo.ViewModels
{
    public class AuthenticatedResponse
    {
        public Member Member { get; set; }
        public string Token { get; set; }
        public bool UserFound { get; set; }
    }
}
