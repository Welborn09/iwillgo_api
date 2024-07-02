using Microsoft.AspNetCore.Mvc;
using IWillGo.Services;


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
            var opportunities = _service.GetOpenOpportunities();
            return Ok(opportunities);
        }
    }
}
