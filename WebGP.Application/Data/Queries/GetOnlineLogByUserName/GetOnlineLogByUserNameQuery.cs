﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGP.Application.Common.Interfaces;
using WebGP.Application.Common.Mapper;
using WebGP.Application.Common.VM;

namespace WebGP.Application.Data.Queries.GetOnlineLogByUserName
{
    public record GetOnlineLogByUserNameQuery(string UserName, DateTime From, DateTime To) : IRequest<IEnumerable<OnlineLogVM>>;
    public class GetOnlineLogByUserNameQueryHandler : IRequestHandler<GetOnlineLogByUserNameQuery, IEnumerable<OnlineLogVM>>
    {
        private readonly IOnlineRepository _onlineRepository;
        public GetOnlineLogByUserNameQueryHandler(IOnlineRepository onlineRepository)
        {
            this._onlineRepository = onlineRepository;
        }

        public async Task<IEnumerable<OnlineLogVM>> Handle(GetOnlineLogByUserNameQuery request, CancellationToken cancellationToken)
            => (await _onlineRepository.GetOnlineLogListByUserNameAsync(request.UserName, request.From, request.To)).Select(v => v.MapToVM());
    }
}
