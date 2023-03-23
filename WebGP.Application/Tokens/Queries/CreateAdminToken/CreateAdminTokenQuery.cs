using System.Security.Cryptography;
using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Domain.SelfEntities;

namespace WebGP.Application.Tokens.Queries.CreateAdminToken;

public class CreateAdminTokenQuery : IRequest<string>
{
    public string? Note { get; set; }
    public required string Role { get; set; }
}

public class CreateAdminTokenQueryHandler : IRequestHandler<CreateAdminTokenQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;
    public CreateAdminTokenQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }
    public async Task<string> Handle(CreateAdminTokenQuery request, CancellationToken cancellationToken)
    {
        var creatorId = _currentUserService.GetCurrentUserId();

        var data = new byte[384];
        RandomNumberGenerator.Fill(data);
        var token = Convert.ToBase64String(data);

        var newAdmin = new Admin()
        {
            Token = token,
            CreatedById = creatorId,
            Note = request.Note ?? "",
            RoleName = request.Role,
            RegistrationTime = DateTime.Now
        };
        await _unitOfWork.AdminRepository.AddAsync(newAdmin);
        await _unitOfWork.Commit();

        return token;
    }
}