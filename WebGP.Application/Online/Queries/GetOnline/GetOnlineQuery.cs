using MediatR;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Application.Online.Queries.GetOnline
{
    public record GetOnlineQuery() : IRequest<int>;
    public class GetOnlineQueryHandler : IRequestHandler<GetOnlineQuery, int>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineQueryHandler(IOnlineRepository onlineRepository)
        {
            _onlineRepository = onlineRepository;
        }

        public Task<int> Handle(GetOnlineQuery request, CancellationToken cancellationToken)
            => _onlineRepository.GetOnlineCountAsync();
    }
}
