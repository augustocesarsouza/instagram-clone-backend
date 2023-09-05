using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.StoryValidator
{
    public class StoryDTOValidator : AbstractValidator<StoryDTO>
    {
        public StoryDTOValidator()
        {
            RuleFor(x => x.Url)
                .NotNull()
                .NotEmpty()
                .WithMessage("Url deve ser informada");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("deve ser informado authorId");
        }
    }
}
