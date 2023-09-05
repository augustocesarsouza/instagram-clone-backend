using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.PostLikeValidator
{
    public class PostLikeDTOValidator : AbstractValidator<PostLikeDTO>
    {
        public PostLikeDTOValidator()
        {
            RuleFor(x => x.AuthorId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("author deve ser informado");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("post deve ser informado");
        }
    }
}
