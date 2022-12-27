using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebGP.Application.Data.Queries.GetTimedByUUID
{
    public partial class GetTimedByUUIDQueryValidator : AbstractValidator<GetTimedByUUIDQuery>
    {
        public GetTimedByUUIDQueryValidator()
        {
            RuleFor(v => v.UUID)
                .Must(v => v is not string uuid 
                    || UUID_REGEX().IsMatch(uuid));
        }

        [GeneratedRegex("^[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}$", RegexOptions.Compiled)]
        private static partial Regex UUID_REGEX();
    }
}
