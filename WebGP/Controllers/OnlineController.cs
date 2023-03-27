using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public Task<int> GetOnline()
        => _mediator.Send(new GetOnlineCountQuery());

    [HttpGet("timed_id")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<int, OnlineVm>> GetByTimedId()
        => _mediator.Send(new GetOnlineByTimedIdQuery(null));

    [HttpGet("timed_id/{timed_id}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<int, OnlineVm>> GetByTimedId(
        [FromRoute(Name = "timed_id")] int timedId)
        => _mediator.Send(new GetOnlineByTimedIdQuery(timedId));

    [HttpGet("uuid")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<string, OnlineVm>> GetByUuid()
        => _mediator.Send(new GetOnlineByUuidQuery(null));

    [HttpGet("uuid/{uuid}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<string, OnlineVm>> GetByUuid(
        [FromRoute(Name = "uuid")] string uuid)
        => _mediator.Send(new GetOnlineByUuidQuery(uuid));

    [HttpGet("static_id")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<uint, OnlineVm>> GetByStaticId()
        => _mediator.Send(new GetOnlineByStaticIdQuery(null));

    [HttpGet("static_id/{static_id}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<uint, OnlineVm>> GetByStaticId(
        [FromRoute(Name = "static_id")] uint staticId)
        => _mediator.Send(new GetOnlineByStaticIdQuery(staticId));
}