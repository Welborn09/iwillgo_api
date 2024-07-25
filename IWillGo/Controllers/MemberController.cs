using IWillGo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;

using IWillGo.ViewModels;
using IWillGo.Authentication.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using IWillGo.DataAccess;
using IWillGo.Authentication;
using IWillGo.Model;

namespace IWillGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        IMemberService memberService;

        IConfiguration configuration;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public MemberController(IConfiguration _configuration, IMemberService _memberService, IJwtAuthenticationManager jwtAuthenticationManager) 
        {
            this.memberService = _memberService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            configuration = _configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            NameValueCollection options = HttpUtility.ParseQueryString(Request.QueryString.ToProperString());
            return Ok(await memberService.ListAsync(options));
        }

        [HttpPost]
        [Route("SaveMember")]
        public async Task<IActionResult> Post([FromBody] RegisterMember member)
        {
            try
            {
                var _member = await memberService.SaveAsync(member);

                var token = jwtAuthenticationManager.Authenticate(_member.Email, _member.Password);

                return Ok(token);
            }
            catch (MissingMemberException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("MemberLogin")]
        public async Task<IActionResult> MemberLogin([FromBody] AuthenticateUser member)
        {
            try
            {
                var authenticatedResponse = new AuthenticatedResponse();

                authenticatedResponse = jwtAuthenticationManager.Authenticate(member.Email, member.Password).Result;

                var key = Encoding.ASCII.GetBytes(configuration["JwtOptions:SigningKey"]);
                //Encoding.ASCII.GetBytes(JwtAuthenticationManager.GenerateKey(256)); 
                //Encoding.ASCII.GetBytes(configuration["JwtOptions:SigningKey"]);
                var secretKey = new SymmetricSecurityKey(key);
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, member.Email)
                };
                var tokeOptions = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                authenticatedResponse.Token = tokenString;
                return Ok(authenticatedResponse);
                /*var authenticatedUser = await memberService.ValidateUser(member.Email, member.Password);

                if (authenticatedUser == null)
                {
                    authenticatedResponse.UserFound = false;
                    return Ok(authenticatedResponse);
                }

                authenticatedResponse.Member = authenticatedUser;
                authenticatedResponse.UserFound = true;

                var key = Encoding.ASCII.GetBytes(configuration["JwtOptions:SigningKey"]);
                var secretKey = new SymmetricSecurityKey(key);
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                authenticatedResponse.Token = tokenString;*/

                //return Ok(authenticatedResponse);
            }
            catch (MissingMemberException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetCount/{eventId}")]
        public async Task<IActionResult> GetMemberCount(string eventId)
        {
            NameValueCollection options = HttpUtility.ParseQueryString(Request.QueryString.ToProperString());
            return Ok(await memberService.GetMemberCount(eventId));
        }

        [HttpPost]
        [Authorize]
        [Route("MemberLogout")]
        public async Task<IActionResult> MemberLogout()
        {
            try
            {
                await HttpContext.SignOutAsync("MemberSession");
                Response.Cookies.Delete("MemberSession");

                return Ok();
            }
            catch (MissingMemberException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
