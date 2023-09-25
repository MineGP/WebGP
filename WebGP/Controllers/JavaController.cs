using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Data.Queries.Java;

namespace WebGP.Controllers;

[Route("java")]
[ApiController]
public class JavaController : ControllerBase
{
    private readonly IMediator _mediator;

    public JavaController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("version/check")]
    [Authorize(Policy = "script_access")]
    public Task<bool> CheckVersion(
        [FromQuery(Name = "game")] string gameVersion,
        [FromQuery(Name = "build")] int buildVersion,
        CancellationToken cancellationToken)
        => _mediator.Send(new CheckJavaVersionQuery(gameVersion, buildVersion), cancellationToken);

    [HttpGet("version/update")]
    [Authorize(Policy = "script_access")]
    public Task<ActionResult> UpdateVersion(
        [FromQuery(Name = "game")] string gameVersion,
        [FromQuery(Name = "build")] int buildVersion,
        CancellationToken cancellationToken)
        => SendFramesQuery(new UpdateJavaVersionQuery(gameVersion, buildVersion), cancellationToken);

    [HttpPost("version/apply")]
    [Authorize(Policy = "script_access")]
    public Task<ActionResult> ApplyVersion(
        [FromQuery(Name = "game")] string gameVersion,
        [FromQuery(Name = "build")] int buildVersion,
        CancellationToken cancellationToken)
        => SendFramesQuery(new ApplyJavaVersionQuery(gameVersion, buildVersion, Request.Body), cancellationToken);

    [HttpGet("version/apply")]
    [Authorize(Policy = "script_access")]
    public async Task<ActionResult> GetApplyVersion(
        [FromQuery(Name = "name")] string name,
        CancellationToken cancellationToken)
        => await _mediator.Send(new GetApplyJavaVersionQuery(name), cancellationToken) is Stream stream
        ? File(stream, MimeTypes.GetMimeType(".jar"))
        : NotFound();

    private async Task<ActionResult> SendFramesQuery(IStreamRequest<IFrame> query, CancellationToken cancellationToken)
    {
        Response.StatusCode = 200;
        Response.ContentType = "text/event-stream";
        Response.Headers.CacheControl = "no-cache";
        Response.Headers.Connection = "keep-alive";

        (IFrame frame, DateTime time)? last = null;
        await foreach (IFrame frame in _mediator.CreateStream(query, cancellationToken))
        {
            DateTime now = DateTime.Now;
            if (last is not (IFrame lastFrame, DateTime lastTime) || now < lastTime || frame.IsRequired(lastFrame))
            {
                await Response.WriteAsync(frame.FormatLine, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
            last = (frame, now.AddSeconds(0.25));
        }

        return Ok();
    }
}
