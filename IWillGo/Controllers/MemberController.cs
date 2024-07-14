using IWillGo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;

using IWillGo.ViewModels;
using IWillGo.Authentication.Interfaces;
using System.Text;

namespace IWillGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        IMemberService memberService;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public MemberController(IMemberService _memberService, IJwtAuthenticationManager jwtAuthenticationManager) 
        {
            this.memberService = _memberService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
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
                var token = jwtAuthenticationManager.Authenticate(member.Email, member.Password);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(new { Token = token });
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
    }
}
