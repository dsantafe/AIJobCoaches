namespace AIJobCoaches.API.Validators
{
    using AIJobCoaches.Application.DTOs;
    using FluentValidation;

    public class QuizResultValidator : AbstractValidator<QuizResultDTO>
    {
        public QuizResultValidator()
        {
            RuleFor(model => model.QuizID)
                .NotNull()
                .WithMessage("The field EmployeeID is mandatory");

            RuleFor(model => model.EmployeeID)
                .NotNull()
                .WithMessage("The field EmployeeID is mandatory");

            RuleFor(model => model.TrainingID)
                .NotNull()
                .WithMessage("The field TrainingID is mandatory");

            RuleFor(model => model.TopicID)
                .NotNull()
                .WithMessage("The field TopicID is mandatory");

            RuleFor(x => x.QuizResponses)
            .NotEmpty().WithMessage("At least one response is required.")
            .Must(r => r.Count <= 10).WithMessage("You cannot submit more than 10 responses.");

            RuleForEach(x => x.QuizResponses).SetValidator(new QuizResponseValidator());
        }
    }
}
