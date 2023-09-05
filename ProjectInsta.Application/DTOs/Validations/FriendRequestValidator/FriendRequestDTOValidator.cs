using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.FriendRequestValidator
{
    public class FriendRequestDTOValidator : AbstractValidator<FriendRequestDTO>
    {
        public FriendRequestDTOValidator()
        {
            RuleFor(x => x.SenderId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("SenderId Não pode ser nulo e deve ser maior que zero");

            RuleFor(x => x.RecipientId)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("RecipientId Não pode ser nulo e deve ser maior que zero");
        }
    }
}
