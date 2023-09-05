using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.SubCommentValidator
{
    public class SubCommentDTOValidator : AbstractValidator<SubCommentDTO>
    {
        public SubCommentDTOValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .NotNull()
                .WithMessage("Tem que contexo um texto para subComment");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("UserId deve ser maior que zero");

            RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("CommentId deve ser maior que zero");
        }
    }
}
