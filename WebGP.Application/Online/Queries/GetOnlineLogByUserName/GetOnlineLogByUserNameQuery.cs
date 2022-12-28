using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.Mapper;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Online.Queries.GetOnlineLogByUserName
{
    public record GetOnlineLogByUserNameQuery(string UserName, DateTime From, DateTime To) : IRequest<IEnumerable<OnlineLogVM>>;
    public class GetOnlineLogByUserNameQueryHandler : IRequestHandler<GetOnlineLogByUserNameQuery, IEnumerable<OnlineLogVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineLogByUserNameQueryHandler(IOnlineRepository onlineRepository)
        {
            _onlineRepository = onlineRepository;
        }

        public async Task<IEnumerable<OnlineLogVM>> Handle(GetOnlineLogByUserNameQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineLogListByUserNameAsync(request.UserName, request.From, request.To)).Select(v => v.MapToVM());
    }
}
