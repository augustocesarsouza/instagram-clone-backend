using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.PostValidator
{
    public class PostDTOValidator : AbstractValidator<PostDTO>
    {
        public PostDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Titulo da publicação Obrigatorio");

            RuleFor(x => x.Url)
                .NotEmpty()
                .NotNull()
                .WithMessage("Url da publicação Obrigatorio");
        }
    }
}
