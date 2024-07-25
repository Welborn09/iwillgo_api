using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IWillGo.Authentication.Interfaces;
using IWillGo.Services.Interfaces;
using IWillGo.Services;
using IWillGo.ViewModels;
using System.Security.Cryptography;

namespace IWillGo.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string key;
        private readonly IMemberService _service;
        public JwtAuthenticationManager(string key, IMemberService memberService)
        {
            this.key = key;
            this._service = memberService;
        }

        public async Task<AuthenticatedResponse> Authenticate(string email, string password)
        {
            var authenticatedResponse = new AuthenticatedResponse();
            var authenticatedUser = await _service.ValidateUser(email, password);

            if (authenticatedUser == null)
            {
                authenticatedResponse.UserFound = false;
                return authenticatedResponse;
            }

            authenticatedResponse.Member = authenticatedUser;
            authenticatedResponse.UserFound = true;

            /*var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            //Encoding.ASCII.GetBytes(GenerateKey(256)); 
            //Encoding.ASCII.GetBytes(GenerateKey(256));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            authenticatedResponse.Token = tokenHandler.WriteToken(token);*/
            return authenticatedResponse;
        }

        public static string GenerateKey(int size)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var key = new byte[32];
                rng.GetBytes(key);
                return Convert.ToBase64String(key); ;
            }
        }
    }
}
