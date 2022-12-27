using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetTimedByID;

namespace WebGP.Controllers
{
    [Route("timed")]
    [ApiController]
    public class TimedController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TimedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("timed_id")]
        [Authorize(Roles = "admin")]
        public Task<IEnumerable<TimedVM>> GetByID([FromQuery(Name = "timed_id")] int timed_id)
        {
            return _mediator.Send(new GetTimedByIDQuery()
            {
                TimedID = timed_id
            });
        }
    }
}
