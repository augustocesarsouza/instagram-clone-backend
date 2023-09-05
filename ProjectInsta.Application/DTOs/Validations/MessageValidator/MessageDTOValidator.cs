using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.MessageValidator
{
    public class MessageDTOValidator : AbstractValidator<MessageDTO>
    {
        public MessageDTOValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .WithMessage("Deve ser informado um texto para mensagem");

            RuleFor(x => x.SenderId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Sender deve ser maior que zero");

            RuleFor(x => x.RecipientId)
               .GreaterThan(0)
               .NotNull()
               .NotEmpty()
               .WithMessage("recipient deve ser maior que zero");
        }
    }
}
