using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetDiscordBy;

namespace WebGP.Controllers;

[Route("discord")]
[ApiController]
public class DiscordController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("id/{id?}")]
    public Task<IDictionary<long, DiscordVM>> GetByID(
        [FromRoute(Name = "id")] long? id = null)
        => _mediator.Send(new GetDiscordByIDQuery(id));
}