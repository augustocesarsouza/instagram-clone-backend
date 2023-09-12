using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.MessageReelValidator
{
    public class MessageReelDTOValidator : AbstractValidator<MessageReelDTO>
    {
        public MessageReelDTOValidator()
        {
            RuleFor(x => x.MessageId)
                .NotEmpty()
                .NotNull()
                .LessThan(1)
                .WithMessage("verifique MessageId não pode ser null, empty, ou menor que 1");

            RuleFor(x => x.ReelId)
                .NotEmpty()
                .NotNull()
                .LessThan(1)
                .WithMessage("verifique MessageId não pode ser null, empty, ou menor que 1");
        }
    }
}
