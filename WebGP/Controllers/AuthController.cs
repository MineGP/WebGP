using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Tokens.Queries.CreateNewToken;
using WebGP.Application.Tokens.Queries.GetAccessToken;

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
    public Task<string> CreateNewUserToken(
        [FromBody] CreateNewTokenQuery model)
        => _mediator.Send(model);

    [HttpPost("get_access")]
    [AllowAnonymous]
    public Task<string> GetNewAccessToken(
        [FromBody] GetAccessTokenQuery model)
        => _mediator.Send(model);

}