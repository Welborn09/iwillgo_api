using IWillGo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;

using IWillGo.ViewModels;

namespace IWillGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        IMemberService memberService;
        public MemberController(IMemberService _memberService) 
        {
            this.memberService = _memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            NameValueCollection options = HttpUtility.ParseQueryString(Request.QueryString.ToProperString());
            return Ok(await memberService.ListAsync(options));
        }

        [HttpPost]
        [Route("SaveMember")]
        public async Task<IActionResult> Post([FromBody] Member member)
        {
            try
            {
                return Ok(await memberService.SaveAsync(member));
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
