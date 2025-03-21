namespace AIJobCoaches.API.Validators
{
    using AIJobCoaches.Application.DTOs;
    using FluentValidation;

    public class QuizResponseValidator : AbstractValidator<QuizResponseDTO>
    {
        public QuizResponseValidator()
        {
            RuleFor(x => x.Question)
                .NotNull()
                .WithMessage("Question is mandatory.");

            RuleFor(x => x.SelectedOption)
                .NotNull()
                .WithMessage("Selected option is mandatory.");

            RuleFor(x => x.CorrectOption)
                .NotNull()
                .WithMessage("Selected option is mandatory.");

            RuleFor(x => x.IsCorrect)
                .NotNull()
                .WithMessage("Is Correct is mandatory.");
        }
    }
}
