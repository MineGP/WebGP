using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Application.Tokens.Queries.CreateNewToken
{
    public class CreateNewTokenQuery : IRequest<string>
    {
        public IEnumerable<string> Roles { get; set; } = null!;
        public int Days { get; set; }
    }
    public class CreateNewTokenQueryHandler : IRequestHandler<CreateNewTokenQuery, string>
    {
        private readonly IJwtService _jwtService;
        public CreateNewTokenQueryHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        public Task<string> Handle(CreateNewTokenQuery request, CancellationToken cancellationToken)
        {
            var builder = _jwtService.GetJwtBuilder();

            foreach (var role in request.Roles)
            {
                builder.AddRole(role);
            }

            return Task.FromResult(builder.Build(TimeSpan.FromDays(request.Days)));
        }
    }
}
