using Microsoft.AspNetCore.Mvc;
using TechnicalTask.BLL.Mediatr.Ping;

namespace TechnicalTask.WebApi.Controllers.Ping
{
    public class PingController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> Ping()
        {
            return HandleResult(await Mediator.Send(new PingQuery()));
        }
    }
}
