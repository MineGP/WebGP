using System.Collections.Concurrent;
using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.Online;
public record GetOnlineByTimedIdQuery(int? TimedId) : IRequest<IDictionary<int, OnlineVm>>;

public class GetOnlineByTimedIdQueryHandler : IRequestHandler<GetOnlineByTimedIdQuery, IDictionary<int, OnlineVm>>
{
    private readonly IOnlineRepository _onlineRepository;

    public GetOnlineByTimedIdQueryHandler(IOnlineRepository onlineRepository)
    {
        _onlineRepository = onlineRepository;
    }

    public async Task<IDictionary<int, OnlineVm>> Handle(GetOnlineByTimedIdQuery request, CancellationToken cancellationToken)
    {
        if (request.TimedId is null) return await _onlineRepository.GetAllOnlineByTimedIdAsync(cancellationToken);

        var online = await _onlineRepository.GetOnlineByTimedIdAsync((int)request.TimedId, cancellationToken);
        if (online is null) return new ConcurrentDictionary<int, OnlineVm>();

        return new Dictionary<int, OnlineVm>()
        {
            [(int)request.TimedId] = online
        };
    }
}