using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.Online;
public record GetOnlineByUuidQuery(ContextType Type, string? Uuid) : IRequest<IDictionary<string, OnlineVm>>;

public class GetOnlineByUuidQueryHandler : IRequestHandler<GetOnlineByUuidQuery, IDictionary<string, OnlineVm>>
{
    private readonly IOnlineRepository _onlineRepository;

    public GetOnlineByUuidQueryHandler(IOnlineRepository onlineRepository)
    {
        _onlineRepository = onlineRepository;
    }

    public async Task<IDictionary<string, OnlineVm>> Handle(GetOnlineByUuidQuery request, CancellationToken cancellationToken)
    {
        if (request.Uuid is null) return await _onlineRepository.GetAllOnlineByUuidAsync(request.Type, cancellationToken);

        var online = await _onlineRepository.GetOnlineByUuidAsync(request.Type, request.Uuid!, cancellationToken);

        if (online is null) return new Dictionary<string, OnlineVm>();

        return new Dictionary<string, OnlineVm>()
        {
            [request.Uuid] = online
        };
    }
}