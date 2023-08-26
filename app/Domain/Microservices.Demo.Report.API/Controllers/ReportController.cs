using Microservices.Demo.Report.API.Aplication;
using Microservices.Demo.Report.API.CQRS.Queries.Infraestucture.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Report.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportApplicationService _reportApplicationService;

        public ReportController(IReportApplicationService reportApplicationService)
        {
            _reportApplicationService = reportApplicationService;
        }

        [HttpGet("policies")]
        public async Task<ActionResult> Get([FromHeader] string AgentLogin)
        {
            return new JsonResult(await _reportApplicationService.GetPolicies(AgentLogin));
        }
    }
}