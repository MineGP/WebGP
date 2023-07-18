using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.Online;

public record GetOnlineByStaticIdQuery(ContextType Type, uint? staticId) : IRequest<IDictionary<uint, OnlineVm>>;

public class GetOnlineByStaticIdQueryHandler : IRequestHandler<GetOnlineByStaticIdQuery, IDictionary<uint, OnlineVm>>
{
    private readonly IOnlineRepository _onlineRepository;

    public GetOnlineByStaticIdQueryHandler(IOnlineRepository onlineRepository)
    {
        _onlineRepository = onlineRepository;
    }

    public async Task<IDictionary<uint, OnlineVm>> Handle(GetOnlineByStaticIdQuery request, CancellationToken cancellationToken)
    {
        if (request.staticId is null) return await _onlineRepository.GetAllOnlineByStaticIdAsync(request.Type, cancellationToken);

        var online = await _onlineRepository.GetOnlineByStaticIdAsync(request.Type, (uint)request.staticId, cancellationToken);
        if (online is null) return new Dictionary<uint, OnlineVm>();
        return new Dictionary<uint, OnlineVm>()
        {
            [(uint)request.staticId] = online
        };
    }
}