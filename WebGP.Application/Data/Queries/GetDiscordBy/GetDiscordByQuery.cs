using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetDiscordBy;

public record GetDiscordByIDQuery(long? DiscordID) : IRequest<IDictionary<long, DiscordVM>>;

public class GetDiscordByQueryHandler :
    IRequestHandler<GetDiscordByIDQuery, IDictionary<long, DiscordVM>>
{
    private readonly IContext _context;

    public GetDiscordByQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<IDictionary<long, DiscordVM>> Handle(GetDiscordByIDQuery request,
        CancellationToken cancellationToken)
    {
        return await GetBy()
            .Where(request.DiscordID is long id ? v => v.DiscordID == id : v => true)
            .ToAsyncEnumerable()
            .ToDictionaryAsync(v => v.DiscordID, v => v, cancellationToken);
    }

    private IQueryable<DiscordVM> GetBy()
    {
        return _context.Users
            .Join(_context.Discords, user => user.Uuid, discord => discord.Uuid,
                (User, Discord) => new { User, Discord })
            .Join(
                _context.RoleWorkReadonlies.Where(role => role.Type == "ROLE"),
                v => v.User == null ? 0 : v.User.Work ?? 0,
                r => r.Id,
                (v, Role) => new { v.User, v.Discord, Role })
            .Join(
                _context.RoleWorkReadonlies.Where(role => role.Type == "WORK"),
                v => v.User == null ? 0 : v.User.Work ?? 0,
                r => r.Id,
                (v, Work) => new { v.User, v.Discord, v.Role, Work })
            .Select(v => new DiscordVM
            {
                DiscordID = v.Discord.DiscordId,
                Exp = v.User.Exp,
                Phone = v.User.Phone,
                Role = v.Role.Name,
                Work = v.Work.Name
            });
    }
}