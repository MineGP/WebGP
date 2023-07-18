using MediatR;
using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetOnlineLogBy;

public record GetOnlineLogByStaticIdQuery(ContextType Type, int? StaticId, DateOnly From, DateOnly To) : 
    IRequest<IDictionary<int, IEnumerable<OnlineLogVM>>>;


public class GetOnlineLogByStaticIdQueryHandler :
    IRequestHandler<GetOnlineLogByStaticIdQuery, IDictionary<int, IEnumerable<OnlineLogVM>>>
{
    public GetOnlineLogByStaticIdQueryHandler(IContextGPO contextGPO, IContextGPC contextGPC)
    {
        _contextGPO = contextGPO;
        _contextGPC = contextGPC;
    }

    private readonly IContextGPO _contextGPO;
    private readonly IContextGPC _contextGPC;

    private IContext GetBy(ContextType database) => database switch
    {
        ContextType.GPO => _contextGPO,
        ContextType.GPC => _contextGPC,
        _ => throw new NotSupportedException($"ContextType '{database}' not support")
    };

    public async Task<IDictionary<int, IEnumerable<OnlineLogVM>>> Handle(GetOnlineLogByStaticIdQuery request, CancellationToken cancellationToken)
    {
        if (request.StaticId is null)
        {
            return await GetBy(request.Type)
                .OnlineLogs
                .Where(v => v.Day >= request.From && v.Day <= request.To)
                .Join(GetBy(request.Type).Users, log => log.Id, usr => usr.Id, (log, usr) => new { Log = log, User = usr })
                .GroupBy(v => v.User.Id)
                .ToDictionaryAsync(v => v.Key, v => v
                        .Select(info => new OnlineLogVM
                        {
                            Day = info.Log.Day.ToString("yyyy-MM-dd"),
                            Seconds = info.Log.Sec
                        }),
                    cancellationToken: cancellationToken);
        }

        var user = await GetBy(request.Type).Users.SingleOrDefaultAsync(v => v.Id == request.StaticId, cancellationToken);
        if (user is null) return new Dictionary<int, IEnumerable<OnlineLogVM>>();

        var logs = await GetBy(request.Type)
            .OnlineLogs
            .Where(v => v.Day >= request.From && v.Day <= request.To)
            .Where(v => v.Id == user.Id)
            .Select(v => new OnlineLogVM
            {
                Day = v.Day.ToString("yyyy-MM-dd"),
                Seconds = v.Sec
            })
            .ToListAsync(cancellationToken);
        return new Dictionary<int, IEnumerable<OnlineLogVM>>()
        {
            [user.Id] = logs
        };
    }
}