using System.Security.Authentication;
using MediatR;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Application.Tokens.Queries.GetAccessToken;

public record GetAccessTokenQuery(string Token) : IRequest<string>;

public class GetAccessTokenQueryHandler : IRequestHandler<GetAccessTokenQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;
    public GetAccessTokenQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var adminRepository = _unitOfWork.AdminRepository;
        var user = await adminRepository.GetByTokenAsync(request.Token);

        if (user is null) throw new AuthenticationException("User not found");
        var builder = _jwtService.GetJwtBuilder();
        return builder
            .AddId(user.Id.ToString())
            .AddRole(user.RoleName)
            .Build(TimeSpan.FromHours(8));
    }
}