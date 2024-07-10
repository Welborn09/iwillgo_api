using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IWillGo.Authentication.Interfaces;
using IWillGo.Services.Interfaces;

namespace IWillGo.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
    {
        { "test", "password" } // Replace with your own user store
    };

        private readonly string key;
        private readonly IMemberService _service;
        public JwtAuthenticationManager(string key, IMemberService memberService)
        {
            this.key = key;
            this._service = memberService;
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var ret = await _service.Login(email, password);

            if (!ret)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
