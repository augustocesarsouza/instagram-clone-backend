using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.CommentValidator
{
    public class CommentDTOValidator : AbstractValidator<CommentDTO>
    {
        public CommentDTOValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .NotNull()
                .WithMessage("Deve ser informado um Text");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("userId deve ser maior que zero");

            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId deve ser maior que zero");

        }
    }
}
