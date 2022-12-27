using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetTimedByID
{
    public record GetTimedByIDQuery(int? TimedID) : IRequest<IDictionary<int, OnlineVM>>;

    public class GetTimedByIDQueryHandler : IRequestHandler<GetTimedByIDQuery, IDictionary<int, OnlineVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetTimedByIDQueryHandler(IOnlineRepository onlineRepository)
        {
            this._onlineRepository = onlineRepository;
        }

        public async Task<IDictionary<int, OnlineVM>> Handle(GetTimedByIDQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineListAsync())
                .Where(v => request.TimedID is not int id || v.TimedID == id)
                .ToDictionary(v => v.TimedID, v => v);
    }
}
