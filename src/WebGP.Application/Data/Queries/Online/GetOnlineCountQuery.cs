using MediatR;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Application.Data.Queries.Online;

public record GetOnlineCountQuery(ContextType Type) : IRequest<int>;

public class GetOnlineCountQueryHandler : IRequestHandler<GetOnlineCountQuery, int>
{
    private readonly IOnlineRepository _onlineRepository;

    public GetOnlineCountQueryHandler(IOnlineRepository onlineRepository)
    {
        _onlineRepository = onlineRepository;
    }

    public Task<int> Handle(GetOnlineCountQuery request, CancellationToken cancellationToken)
    {
        return _onlineRepository.GetOnlineCountAsync(request.Type, cancellationToken);
    }
}