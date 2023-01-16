using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetOnlineBy;

public record GetOnlineCountQuery : IRequest<int>;
public record GetOnlineByTimedIDQuery(int? TimedID) : IRequest<IDictionary<int, OnlineVM>>;
public record GetOnlineByUUIDQuery(string? UUID) : IRequest<IDictionary<string, OnlineVM>>;

public class GetOnlineByQueryHandler :
    IRequestHandler<GetOnlineCountQuery, int>,
    IRequestHandler<GetOnlineByTimedIDQuery, IDictionary<int, OnlineVM>>,
    IRequestHandler<GetOnlineByUUIDQuery, IDictionary<string, OnlineVM>>
{
    private readonly IContext _context;

    public GetOnlineByQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<IDictionary<int, OnlineVM>> Handle(GetOnlineByTimedIDQuery request,
        CancellationToken cancellationToken)
        => await GetList()
            .Where(request.TimedID is int timedID ? v => v.TimedID == timedID : v => true)
            .ToAsyncEnumerable()
            .ToDictionaryAsync(v => v.TimedID, v => v, cancellationToken);

    public async Task<IDictionary<string, OnlineVM>> Handle(GetOnlineByUUIDQuery request,
        CancellationToken cancellationToken)
        => await GetList()
            .Where(request.UUID is string uuid ? v => v.UUID == uuid : v => true)
            .ToAsyncEnumerable()
            .ToDictionaryAsync(v => v.UUID, v => v, cancellationToken);

    public Task<int> Handle(GetOnlineCountQuery request, CancellationToken cancellationToken)
        => Task.Run(GetList().Count, cancellationToken);

    private IQueryable<OnlineVM> GetList()
        => _context.Onlines
            .Join(_context.Users, online => online.Uuid, user => user.Uuid, (Online, User) => new { User, Online })
            .GroupJoin(_context.Discords, v => v.Online.Uuid, discord => discord.Uuid,
                (v, Discords) => new { v.Online, v.User, Discords })
            .SelectMany(v => v.Discords.DefaultIfEmpty(), (v, Discord) => new { v.Online, v.User, Discord })
            .Join(
                _context.RoleWorkReadonlies.Where(role => role.Type == "ROLE"),
                v => v.User == null ? 0 : v.User.Role,
                r => r.Id,
                (v, Role) => new { v.Online, v.User, v.Discord, Role })
            .Join(
                _context.RoleWorkReadonlies.Where(work => work.Type == "WORK"),
                v => v.User == null ? 0 : v.User.Work ?? 0,
                r => r.Id,
                (v, Work) => new { v.Online, v.User, v.Discord, v.Role, Work })
            .Select(v => new OnlineVM
            {
                TimedID = v.Online.TimedId ?? 0,
                UUID = v.User.Uuid,
                FirstName = v.User.FirstName,
                LastName = v.User.LastName,
                DiscordID = v.Discord == null ? null : v.Discord.DiscordId,
                Role = v.Role.Name,
                Work = v.Work.Name
            });
}