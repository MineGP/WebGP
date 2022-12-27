using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.VM;
using WebGP.Application.Data.Queries.GetDiscordByID;

namespace WebGP.Application.Data.Queries.GetOnline
{
    public record GetOnlineQuery() : IRequest<int>;
    public class GetOnlineQueryHandler : IRequestHandler<GetOnlineQuery, int>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineQueryHandler(IOnlineRepository onlineRepository)
        {
            this._onlineRepository = onlineRepository;
        }

        public Task<int> Handle(GetOnlineQuery request, CancellationToken cancellationToken)
            => _onlineRepository.GetOnlineCountAsync();
    }
}
