using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.Online;

namespace WebGP.Controllers;

[Route("online")]
[ApiController]
public class OnlineController : ControllerBase
{
    private readonly IMediator _mediator;

    public OnlineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "query_access")]
    public Task<int> GetOnline(
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineCountQuery(type), cancellationToken);

    [HttpGet("timed_id")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<int, OnlineVm>> GetByTimedId(
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineByTimedIdQuery(type, null), cancellationToken);

    [HttpGet("timed_id/{timed_id}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<int, OnlineVm>> GetByTimedId(
        [FromRoute(Name = "timed_id")] int timedId,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineByTimedIdQuery(type, timedId), cancellationToken);

    [HttpGet("uuid")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<string, OnlineVm>> GetByUuid(
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineByUuidQuery(type, null), cancellationToken);

    [HttpGet("uuid/{uuid}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<string, OnlineVm>> GetByUuid(
        [FromRoute(Name = "uuid")] string uuid,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineByUuidQuery(type, uuid), cancellationToken);

    [HttpGet("static_id")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<uint, OnlineVm>> GetByStaticId(
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineByStaticIdQuery(type, null), cancellationToken);

    [HttpGet("static_id/{static_id}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<uint, OnlineVm>> GetByStaticId(
        [FromRoute(Name = "static_id")] uint staticId,
        [FromQuery(Name = "type")] ContextType type,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetOnlineByStaticIdQuery(type, staticId), cancellationToken);
}