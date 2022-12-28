using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Online.Queries.GetOnline;
using WebGP.Application.Online.Queries.GetOnlineLogByUserName;
using WebGP.Application.Online.Queries.GetOnlineLogListByStaticID;
using WebGP.Application.Online.Queries.GetOnlineLogListByUUID;

namespace WebGP.Controllers
{
    [Route("online"), ApiController]
    public class OnlineController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OnlineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<int> GetOnline()
            => _mediator.Send(new GetOnlineQuery());

        [HttpGet("static_id/{static_id}")]
        public Task<IEnumerable<OnlineLogVM>> GetByStaticID(
            [FromRoute(Name = "static_id")] uint staticID,
            [FromQuery(Name = "from")] DateTime from,
            [FromQuery(Name = "to")] DateTime to)
            => _mediator.Send(new GetOnlineLogListByStaticIDQuery(staticID, from, to));

        [HttpGet("uuid/{uuid}")]
        public Task<IEnumerable<OnlineLogVM>> GetByUUID(
            [FromRoute(Name = "uuid")] string uuid,
            [FromQuery(Name = "from")] DateTime from,
            [FromQuery(Name = "to")] DateTime to)
            => _mediator.Send(new GetOnlineLogListByUUIDQuery(uuid, from, to));

        [HttpGet("user_name/{user_name}")]
        public Task<IEnumerable<OnlineLogVM>> GetByUserName(
            [FromRoute(Name = "user_name")] string userName,
            [FromQuery(Name = "from")] DateTime from,
            [FromQuery(Name = "to")] DateTime to)
            => _mediator.Send(new GetOnlineLogByUserNameQuery(userName, from, to));
    }
}
