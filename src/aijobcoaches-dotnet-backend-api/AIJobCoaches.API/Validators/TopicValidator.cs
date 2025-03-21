namespace AIJobCoaches.API.Validators
{
    using AIJobCoaches.Application.DTOs;
    using FluentValidation;

    public class TopicValidator : AbstractValidator<TopicDTO>
    {
        public TopicValidator()
        {
            RuleFor(topic => topic.Items)
                .NotNull()
                .WithMessage("The field Items is mandatory")
                .Must(items => items.Count != 0)
                .WithMessage("At least one item is required");
        }
    }
}
