using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGP.Application.Data.Queries.GetDiscordByID
{
    public class GetDiscordByIDQueryValidator : AbstractValidator<GetDiscordByIDQuery>
    {
        public GetDiscordByIDQueryValidator()
        {
            RuleFor(v => v.DiscordID)
                .Must(v => v is not int id || id >= 0);
        }
    }
}
