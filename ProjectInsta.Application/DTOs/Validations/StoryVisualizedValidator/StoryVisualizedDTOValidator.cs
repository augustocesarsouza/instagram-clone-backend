using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.StoryVisualizedValidator
{
    public class StoryVisualizedDTOValidator : AbstractValidator<StoryVisualizedDTO>
    {
        public StoryVisualizedDTOValidator()
        {
            RuleFor(x => x.UserViewedId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserViewedId não pode ser null");

            RuleFor(x => x.UserCreatedPostId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserCreatedPostId não pode ser null");

            RuleFor(x => x.StoryId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("Storyid não pode ser null");
        }
    }
}
