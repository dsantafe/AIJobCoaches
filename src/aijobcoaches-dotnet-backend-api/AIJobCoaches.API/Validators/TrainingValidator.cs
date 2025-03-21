namespace AIJobCoaches.API.Validators
{
    using AIJobCoaches.Application.DTOs;
    using FluentValidation;

    public class TrainingValidator : AbstractValidator<TrainingDTO>
    {
        public TrainingValidator()
        {
            RuleFor(model => model.TrainingName)
                .NotNull()
                .WithMessage("The field TrainingName is mandatory");

            RuleFor(model => model.Description)
                .NotNull()
                .WithMessage("The field Description is mandatory");

            RuleFor(model => model.Topics)
                .NotNull()
                .WithMessage("The field Topics is mandatory")
                .Must(topics => topics.Any())
                .WithMessage("At least one topic is required");

            RuleForEach(model => model.Topics)
                .SetValidator(new TopicValidator());
        }
    }
}
