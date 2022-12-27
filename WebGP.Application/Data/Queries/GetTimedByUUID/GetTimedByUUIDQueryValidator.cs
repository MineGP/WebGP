using FluentValidation;
using System.Text.RegularExpressions;

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
