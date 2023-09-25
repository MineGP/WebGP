using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.Interfaces;
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
    [Authorize(Policy = "query_access")]
    [HttpGet("static_id/")]
    public Task<IDictionary<int, IEnumerable<OnlineLogVM>>> GetByTimedIdAll(
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineLogByStaticIdQuery(type, null, from, to), cancellationToken);

    [Authorize(Policy = "query_access")]
    [HttpGet("static_id/{static_id}")]
    public Task<IDictionary<int, IEnumerable<OnlineLogVM>>> GetByTimedId(
        [FromRoute(Name = "static_id")] int? staticId,
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineLogByStaticIdQuery(type, staticId, from, to), cancellationToken);

    [Authorize(Policy = "query_access")]
    [HttpGet("uuid/")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUuidAll(
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineLogByUuidQuery(type, null, from, to), cancellationToken);

    [Authorize(Policy = "query_access")]
    [HttpGet("uuid/{uuid}")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUuid(
        [FromRoute(Name = "uuid")] string? uuid,
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineLogByUuidQuery(type, uuid, from, to), cancellationToken);

    [Authorize(Policy = "query_access")]
    [HttpGet("user_name/")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUserNameAll(
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineLogByUserNameQuery(type, null, from, to), cancellationToken);

    [Authorize(Policy = "query_access")]
    [HttpGet("user_name/{user_name?}")]
    public Task<IDictionary<string, IEnumerable<OnlineLogVM>>> GetByUserName(
        [FromRoute(Name = "user_name")] string? userName,
        [FromQuery(Name = "from")] DateOnly from,
        [FromQuery(Name = "to")] DateOnly to,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineLogByUserNameQuery(type, userName, from, to), cancellationToken);
}