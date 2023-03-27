using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.Discord;

public record GetAllDiscordsQuery : IRequest<IDictionary<long, DiscordVm>>;

public class GetAllDiscordsQueryHandler : IRequestHandler<GetAllDiscordsQuery, IDictionary<long, DiscordVm>>
{
    private readonly IDiscordRepository _discordRepository;

    public GetAllDiscordsQueryHandler(IDiscordRepository discordRepository)
    {
        _discordRepository = discordRepository;
    }

    public Task<IDictionary<long, DiscordVm>> Handle(GetAllDiscordsQuery request, CancellationToken cancellationToken)
    {
        return _discordRepository.GetAllDiscordsAsync(cancellationToken);
    }
}