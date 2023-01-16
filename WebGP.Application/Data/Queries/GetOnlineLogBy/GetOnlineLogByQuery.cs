using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Application.Data.Queries.GetOnlineLogBy;

public interface IGetOnlineLogByQuery
{
    DateOnly From { get; }
    DateOnly To { get; }
}

public record GetOnlineLogByUserNameQuery
    (string UserName, DateOnly From, DateOnly To) : IRequest<IEnumerable<OnlineLogVM>>, IGetOnlineLogByQuery;

public record GetOnlineLogByStaticIDQuery
    (int? StaticID, DateOnly From, DateOnly To) : IRequest<IDictionary<int, OnlineLogVM>>, IGetOnlineLogByQuery;

public record GetOnlineLogByUUIDQuery
    (string? UUID, DateOnly From, DateOnly To) : IRequest<IDictionary<string, OnlineLogVM>>, IGetOnlineLogByQuery;

public class GetOnlineLogByQueryHandler :
    IRequestHandler<GetOnlineLogByUserNameQuery, IEnumerable<OnlineLogVM>>,
    IRequestHandler<GetOnlineLogByStaticIDQuery, IDictionary<int, OnlineLogVM>>,
    IRequestHandler<GetOnlineLogByUUIDQuery, IDictionary<string, OnlineLogVM>>
{
    private readonly IContext _context;

    public GetOnlineLogByQueryHandler(IContext context)
    {
        _context = context;
    }

    public async Task<IDictionary<int, OnlineLogVM>> Handle(GetOnlineLogByStaticIDQuery request,
        CancellationToken cancellationToken)
    {
        return await GetBy(request)
            .Where(request.StaticID is int id ? v => v.User.Id == id : v => true)
            .ToAsyncEnumerable()
            .ToDictionaryAsync(v => v.User.Id, v => new OnlineLogVM
            {
                Day = v.Log.Day.ToString("yyyy-MM-dd"),
                Seconds = v.Log.Sec
            }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<OnlineLogVM>> Handle(GetOnlineLogByUserNameQuery request,
        CancellationToken cancellationToken)
    {
        return await GetBy(request)
            .Where(v => v.User.UserName == request.UserName)
            .Select((Func<ByInfo, OnlineLogVM>)(v => new OnlineLogVM
            {
                Day = v.Log.Day.ToString("yyyy-MM-dd"),
                Seconds = v.Log.Sec
            }))
            .ToAsyncEnumerable()
            .ToListAsync(cancellationToken);
    }

    public async Task<IDictionary<string, OnlineLogVM>> Handle(GetOnlineLogByUUIDQuery request,
        CancellationToken cancellationToken)
    {
        return await GetBy(request)
            .Where(request.UUID is string uuid ? v => v.User.Uuid == uuid : v => true)
            .ToAsyncEnumerable()
            .ToDictionaryAsync(v => v.User.Uuid, v => new OnlineLogVM
            {
                Day = v.Log.Day.ToString("yyyy-MM-dd"),
                Seconds = v.Log.Sec
            }, cancellationToken: cancellationToken);
    }

    private IQueryable<ByInfo> GetBy(IGetOnlineLogByQuery request)
    {
        return _context.OnlineLogs
            .Where(v => v.Day >= request.From && v.Day <= request.To)
            .Join(_context.Users, log => log.Id, user => user.Id, (log, user) => new ByInfo(log, user));
    }

    private record ByInfo(OnlineLog Log, User User);
}