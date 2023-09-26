using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Data.Queries.Java;
using WebGP.Utils.SSE;

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
    public Task UpdateVersion(
        [FromQuery(Name = "game")] string gameVersion,
        [FromQuery(Name = "build")] int buildVersion,
        CancellationToken cancellationToken)
        => SendFramesQuery(new UpdateJavaVersionQuery(gameVersion, buildVersion), cancellationToken);

    [HttpPost("version/apply")]
    [Authorize(Policy = "script_access")]
    public Task ApplyVersion(
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

    [HttpGet("version/test")]
    [Authorize(Policy = "script_access")]
    public async Task GetTestVersion(CancellationToken cancellationToken)
    {
        await HttpContext.SSEInitAsync(cancellationToken);
        await HttpContext.SSESendAsync(new JObject()
        {
            ["test"] = 12,
            ["any"] = new JArray()
            {
                1,2,3
            }
        }, cancellationToken);
        for (int i = 0; i < 10; i++)
        {
            await HttpContext.SSESendAsync(new PacketSSE(1, "test_event/asd", "data in test event 1"), cancellationToken);
            await HttpContext.SSESendAsync("data in test event 2", cancellationToken);
            await HttpContext.SSESendAsync(new PacketSSE(2, "test", "data in test event 3"), cancellationToken);
            await HttpContext.SSESendAsync(new PacketSSE(null, "any", "data in test event 3"), cancellationToken);
        }
    }

    private async Task SendFramesQuery(IStreamRequest<IFrame> query, CancellationToken cancellationToken)
    {
        await HttpContext.SSEInitAsync(cancellationToken);
        (IFrame frame, DateTime time)? last = null;
        await foreach (IFrame frame in _mediator.CreateStream(query, cancellationToken))
        {
            DateTime now = DateTime.Now;
            if (last is not (IFrame lastFrame, DateTime lastTime) || now > lastTime || frame.IsRequired(lastFrame))
                await HttpContext.SSESendAsync(new PacketSSE(null, frame.Type, frame.Data), cancellationToken);
            last = (frame, now.AddSeconds(0.25));
        }
    }
}
