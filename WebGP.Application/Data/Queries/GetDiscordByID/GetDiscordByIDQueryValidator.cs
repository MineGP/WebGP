using FluentValidation;

namespace WebGP.Application.Data.Queries.GetDiscordByID
{
    public class GetDiscordByIDQueryValidator : AbstractValidator<GetDiscordByIDQuery>
    {
        public GetDiscordByIDQueryValidator()
        {
            RuleFor(v => v.DiscordID)
                .Must(v => v is not long id || id >= 0);
        }
    }
}
