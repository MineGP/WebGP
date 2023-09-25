using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGP.Application.Tokens.Queries.CreateAdminToken;
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
    
    [HttpPost("new_token")]
    [Authorize(Policy = "full_access")]
    public Task<string> CreateNewUserToken(
        [FromBody] CreateAdminTokenQuery model,
        CancellationToken cancellationToken)
        => _mediator.Send(model, cancellationToken);

    [HttpPost("get_access")]
    [AllowAnonymous]
    public Task<string> GetNewAccessToken(
        [FromBody] GetAccessTokenQuery model,
        CancellationToken cancellationToken)
        => _mediator.Send(model, cancellationToken);

}