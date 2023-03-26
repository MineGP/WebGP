using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetOnlineLogBy;

namespace WebGP.Controllers;

[Route("online_log")]
[ApiController]
public class OnlineLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public OnlineLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("static_id/")]
    public Task<IDictionary<int, IEnumerable<OnlineLogVM>>> GetByTimedIdAll(
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to)
        => _mediator.Send(new GetOnlineLogByStaticIdQuery(null, from, to));

    [HttpGet("static_id/{static_id}")]
    public Task<IDictionary<int, IEnumerable<OnlineLogVM>>> GetByTimedId(
        [FromRoute(Name = "static_id")] int? staticId,
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to)
        => _mediator.Send(new GetOnlineLogByStaticIdQuery(staticId, from, to));

    [HttpGet("uuid/")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUuidAll(
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to)
        => _mediator.Send(new GetOnlineLogByUuidQuery(null, from, to));

    [HttpGet("uuid/{uuid}")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUuid(
        [FromRoute(Name = "uuid")] string? uuid,
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to)
        => _mediator.Send(new GetOnlineLogByUuidQuery(uuid, from, to));

    [HttpGet("user_name/")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUserNameAll(
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to)
        => _mediator.Send(new GetOnlineLogByUserNameQuery(null, from, to));

    [HttpGet("user_name/{user_name?}")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUserName(
        [FromRoute(Name = "user_name")] string? userName,
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to)
        => _mediator.Send(new GetOnlineLogByUserNameQuery(userName, from, to));
}