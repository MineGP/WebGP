using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetTimedByID;
using WebGP.Application.Data.Queries.GetTimedByUUID;

namespace WebGP.Controllers
{
    [Route("timed"), ApiController]
    public class TimedController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TimedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("id/{id?}")]
        public Task<IDictionary<int, OnlineVM>> GetByID(
            [FromRoute(Name = "id")] int? id = null)
            => _mediator.Send(new GetTimedByIDQuery(id));

        [HttpGet("uuid/{uuid?}")]
        public Task<IDictionary<int, OnlineVM>> GetByUUID(
            [FromRoute(Name = "uuid")] string? uuid = null)
            => _mediator.Send(new GetTimedByUUIDQuery(uuid));
    }
}
