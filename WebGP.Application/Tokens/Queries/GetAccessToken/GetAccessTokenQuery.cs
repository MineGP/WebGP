using System.Security.Authentication;
using MediatR;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Application.Tokens.Queries.GetAccessToken;

public record GetAccessTokenQuery(string Token) : IRequest<string>;

public class GetAccessTokenQueryHandler : IRequestHandler<GetAccessTokenQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtBuilder;
    public GetAccessTokenQueryHandler(IUnitOfWork unitOfWork, IJwtService jwtBuilder)
    {
        _unitOfWork = unitOfWork;
        _jwtBuilder = jwtBuilder;
    }

    public async Task<string> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var adminRepository = _unitOfWork.AdminRepository;
        var user = await adminRepository.GetByTokenAsync(request.Token);

        if (user is null) throw new AuthenticationException("User not found");
        var jwt = _jwtBuilder.GetJwtBuilder();
        return jwt.AddRole(user.RoleName).Build(TimeSpan.FromHours(8));
    }
}