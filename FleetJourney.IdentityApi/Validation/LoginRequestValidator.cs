using FleetJourney.IdentityApi.Contracts.Requests;
using FluentValidation;

namespace FleetJourney.IdentityApi.Validation;

internal sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotNull();

        RuleFor(x => x.Password)
            .NotNull()
            .Matches(@"^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\D*\d)(?=[^!#%]*[!#%])[A-Za-z0-9!#%]{6,32}");
    }
}