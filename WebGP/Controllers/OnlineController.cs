using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetOnlineBy;

namespace WebGP.Controllers;

[Route("timed")]
[ApiController]
public class OnlineController : ControllerBase
{
    private readonly IMediator _mediator;

    public OnlineController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<int> GetOnline()
    {
        return _mediator.Send(new GetOnlineCountQuery());
    }

    [HttpGet("id/{id?}")]
    public Task<IDictionary<int, OnlineVM>> GetByID(
        [FromRoute(Name = "id")] int? id = null)
    {
        return _mediator.Send(new GetOnlineByTimedIDQuery(id));
    }

    [HttpGet("uuid/{uuid?}")]
    public Task<IDictionary<string, OnlineVM>> GetByUUID(
        [FromRoute(Name = "uuid")] string? uuid = null)
    {
        return _mediator.Send(new GetOnlineByUUIDQuery(uuid));
    }
}