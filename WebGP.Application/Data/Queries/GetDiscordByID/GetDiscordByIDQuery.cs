using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetDiscordByID
{
    public record GetDiscordByIDQuery(long? DiscordID) : IRequest<IDictionary<long, DiscordVM>>;

    public class GetDiscordByIDQueryHandler : IRequestHandler<GetDiscordByIDQuery, IDictionary<long, DiscordVM>>
    {
        private readonly IDiscordRepository _discordRepository;
        public GetDiscordByIDQueryHandler(IDiscordRepository discordRepository)
        {
            this._discordRepository = discordRepository;
        }

        public async Task<IDictionary<long, DiscordVM>> Handle(GetDiscordByIDQuery request, CancellationToken cancellationToken)
            => (await _discordRepository.GetDiscordListAsync())
                .Where(v => request.DiscordID is not long id || v.DiscordID == id)
                .ToDictionary(v => v.DiscordID, v => v);
    }
}
