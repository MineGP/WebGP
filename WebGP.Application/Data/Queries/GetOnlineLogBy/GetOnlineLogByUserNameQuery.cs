using MediatR;
using Microsoft.EntityFrameworkCore;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetOnlineLogBy;
public record GetOnlineLogByUserNameQuery(string? UserName, DateOnly From, DateOnly To) : 
    IRequest<IDictionary<string, IEnumerable<OnlineLogVM>>>;

public class GetOnlineLogByUserNameQueryHandler :
    IRequestHandler<GetOnlineLogByUserNameQuery, IDictionary<string, IEnumerable<OnlineLogVM>>>
{
    private readonly IContext _context;
    public GetOnlineLogByUserNameQueryHandler(IContext context)
    {
        _context = context;
    }
    public async Task<IDictionary<string, IEnumerable<OnlineLogVM>>> Handle(GetOnlineLogByUserNameQuery request, CancellationToken cancellationToken)
    {
        if (request.UserName is null)
        {
            return await _context
                .OnlineLogs
                .Where(v => v.Day >= request.From && v.Day <= request.To)
                .Join(_context.Users, log => log.Id, usr => usr.Id, (log, usr) => new { Log = log, User = usr })
                .GroupBy(v => v.User.UserName)
                .ToDictionaryAsync(v => v.Key, v => v
                    .Select(info => new OnlineLogVM
                    {
                        Day = info.Log.Day.ToString("yyyy-MM-dd"),
                        Seconds = info.Log.Sec
                    }),
                    cancellationToken: cancellationToken);
        }

        var user = await _context.Users.SingleOrDefaultAsync(v => v.UserName == request.UserName, cancellationToken);
        if (user is null) return new Dictionary<string, IEnumerable<OnlineLogVM>>();

        var logs = await _context
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