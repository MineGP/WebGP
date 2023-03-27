using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.Discord;

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

    [HttpGet("id/{id}")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<long, DiscordVm>> GetById(
        [FromRoute(Name = "id")] long id,
        CancellationToken cancellationToken)
        => _mediator.Send(new GetDiscordByIdQuery(id), cancellationToken);

    [HttpGet("id")]
    [Authorize(Policy = "query_access")]
    public Task<IDictionary<long, DiscordVm>> GetAll(CancellationToken cancellationToken)
        => _mediator.Send(new GetAllDiscordsQuery(), cancellationToken);
}