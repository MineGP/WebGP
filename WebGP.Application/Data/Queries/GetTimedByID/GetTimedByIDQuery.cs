using MediatR;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetTimedByID
{
    public class GetTimedByIDQuery : IRequest<IEnumerable<TimedVM>>
    {
        public int TimedID { get; set; }
    }
    public class GetTimedByIDQueryHandler : IRequestHandler<GetTimedByIDQuery, IEnumerable<TimedVM>>
    {
        private readonly ITimedRepository _timedRepository;
        public GetTimedByIDQueryHandler(ITimedRepository timedRepository)
        {
            this._timedRepository = timedRepository;
        }

        public async Task<IEnumerable<TimedVM>> Handle(GetTimedByIDQuery request, CancellationToken cancellationToken)
        {
            return (await _timedRepository.GetTimedAsync()).Where(v => v.TimedID == request.TimedID);
        }
    }
}
