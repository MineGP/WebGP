using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGP.Application.Data.Queries.GetTimedByID
{
    public class GetTimedByIDQueryValidator : AbstractValidator<GetTimedByIDQuery>
    {
        public GetTimedByIDQueryValidator()
        {
            RuleFor(v => v.TimedID)
                .Must(v => v > 10);
        }
    }
}
