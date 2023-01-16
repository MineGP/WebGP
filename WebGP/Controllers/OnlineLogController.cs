using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetOnlineLogBy;

namespace WebGP.Controllers
{
    [Route("online"), ApiController]
    public class OnlineLogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OnlineLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("static_id/{static_id}")]
        public Task<IDictionary<int, OnlineLogVM>> GetByTimedID(
            [FromRoute(Name = "static_id")] int? staticID,
            [FromQuery(Name = "from")] DateOnly from,
            [FromQuery(Name = "to")] DateOnly to)
            => _mediator.Send(new GetOnlineLogByStaticIDQuery(staticID, from, to));

        [HttpGet("uuid/{uuid}")]
        public Task<IDictionary<string, OnlineLogVM>> GetByUUID(
            [FromRoute(Name = "uuid")] string uuid,
            [FromQuery(Name = "from")] DateOnly from,
            [FromQuery(Name = "to")] DateOnly to)
            => _mediator.Send(new GetOnlineLogByUUIDQuery(uuid, from, to));

        [HttpGet("user_name/{user_name}")]
        public Task<IEnumerable<OnlineLogVM>> GetByUserName(
            [FromRoute(Name = "user_name")] string userName,
            [FromQuery(Name = "from")] DateOnly from,
            [FromQuery(Name = "to")] DateOnly to)
            => _mediator.Send(new GetOnlineLogByUserNameQuery(userName, from, to));
    }
}
