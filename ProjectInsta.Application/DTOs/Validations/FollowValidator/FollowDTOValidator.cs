using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.FollowValidator
{
    public class FollowDTOValidator : AbstractValidator<FollowDTO>
    {
        public FollowDTOValidator()
        {
            RuleFor(x => x.FollowerId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Follower deve ser maior que zero");

            RuleFor(x => x.FollowingId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Following deve ser maior que zero");
        }
    }
}
