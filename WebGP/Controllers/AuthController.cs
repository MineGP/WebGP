using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Tokens.Queries.CreateNewToken;

namespace WebGP.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // admin gets new login
    [HttpPost("new_token")]
    [Authorize(Policy = "full_access")]
    public Task<string> CreateNewToken(
        [FromQuery] IEnumerable<string> roles,
        [FromQuery] int day)
        => _mediator.Send(new CreateNewTokenQuery(roles, day));


}