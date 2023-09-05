using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.LikeCommentValidator
{
    public class LikeCommentDTOValidator : AbstractValidator<LikeCommentDTO>
    {
        public LikeCommentDTOValidator()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("AuthorId do Like deve ser informado pls não poder se menor igual zero");

            RuleFor(x => x.CommentId)
                .NotEmpty() 
                .NotNull()
                .GreaterThan(0)
                .WithMessage("CommentId do Like deve ser informado pls não poder se menor igual zero");
        }
    }
}
