namespace AIJobCoaches.API.Validators
{
    using AIJobCoaches.Application.DTOs;
    using FluentValidation;

    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(model => model.Username)
                .NotNull()
                .WithMessage("The field Username is mandatory");

            RuleFor(model => model.Password)
                .NotNull()
                .WithMessage("The field Password is mandatory");
        }
    }
}
