using MediatR;
using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetOnlineLogBy;
public record GetOnlineLogByUserNameQuery(ContextType Type, string? UserName, DateOnly From, DateOnly To) : 
    IRequest<IDictionary<string, IEnumerable<OnlineLogVM>>>;

public class GetOnlineLogByUserNameQueryHandler :
    IRequestHandler<GetOnlineLogByUserNameQuery, IDictionary<string, IEnumerable<OnlineLogVM>>>
{
    private readonly IContextGPO _contextGPO;
    private readonly IContextGPC _contextGPC;

    public GetOnlineLogByUserNameQueryHandler(IContextGPO contextGPO, IContextGPC contextGPC)
    {
        _contextGPO = contextGPO;
        _contextGPC = contextGPC;
    }

    private IContext GetBy(ContextType database) => database switch
    {
        ContextType.GPO => _contextGPO,
        ContextType.GPC => _contextGPC,
        _ => throw new NotSupportedException($"ContextType '{database}' not support")
    };
    public async Task<IDictionary<string, IEnumerable<OnlineLogVM>>> Handle(GetOnlineLogByUserNameQuery request, CancellationToken cancellationToken)
    {
        if (request.UserName is null)
        {
            return await GetBy(request.Type)
                .OnlineLogs
                .Where(v => v.Day >= request.From && v.Day <= request.To)
                .Join(GetBy(request.Type).Users, log => log.Id, usr => usr.Id, (log, usr) => new { Log = log, User = usr })
                .GroupBy(v => v.User.UserName)
                .ToDictionaryAsync(v => v.Key, v => v
                    .Select(info => new OnlineLogVM
                    {
                        Day = info.Log.Day.ToString("yyyy-MM-dd"),
                        Seconds = info.Log.Sec
                    }),
                    cancellationToken: cancellationToken);
        }

        var user = await GetBy(request.Type).Users.SingleOrDefaultAsync(v => v.UserName == request.UserName, cancellationToken);
        if (user is null) return new Dictionary<string, IEnumerable<OnlineLogVM>>();

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

        return new Dictionary<string, IEnumerable<OnlineLogVM>>()
        {
            [user.UserName] = logs
        };
    }
}