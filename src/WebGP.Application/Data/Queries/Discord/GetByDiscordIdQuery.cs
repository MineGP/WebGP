using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.Discord;

public record GetDiscordByIdQuery(ContextType Type, long DiscordId) : IRequest<IDictionary<long, DiscordVm>>;

public class GetDiscordByQueryHandler :
    IRequestHandler<GetDiscordByIdQuery, IDictionary<long, DiscordVm>>
{
    private readonly IDiscordRepository _discordRepository;

    public GetDiscordByQueryHandler(IDiscordRepository discordRepository)
    {
        _discordRepository = discordRepository;
    }

    public async Task<IDictionary<long, DiscordVm>> Handle(GetDiscordByIdQuery request, CancellationToken cancellationToken)
    {
        var discord = await _discordRepository.GetByDiscordIdAsync(request.Type, request.DiscordId, cancellationToken);
        if (discord == null) return new Dictionary<long, DiscordVm>();
        return new Dictionary<long, DiscordVm>()
        {
            [discord.DiscordId] = discord,
        };
    }
}