using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;
using IWillGo.Services;
using IWillGo.Services.Interfaces;
using Microsoft.Extensions.Options;


namespace IWillGo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunitiesService _service;

        public OpportunitiesController(IOpportunitiesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetOpportunities()
        {
            NameValueCollection options = HttpUtility.ParseQueryString(Request.QueryString.ToProperString());
            var opportunities = await _service.GetOpenOpportunities(options);
            return Ok(opportunities);
        }

        [HttpGet]
        [Route("event/{eventId}")]
        public async Task<IActionResult> GetEvent(string eventId)
        {            
            return Ok(await _service.GetOpportunity(eventId));
        }
    }
}
