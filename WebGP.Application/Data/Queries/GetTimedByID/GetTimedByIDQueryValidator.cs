using FluentValidation;

namespace WebGP.Application.Data.Queries.GetTimedByID
{
    public class GetTimedByIDQueryValidator : AbstractValidator<GetTimedByIDQuery>
    {
        public GetTimedByIDQueryValidator()
        {
            RuleFor(v => v.TimedID)
                .Must(v => v is not int id || id >= 0);
        }
    }
}
