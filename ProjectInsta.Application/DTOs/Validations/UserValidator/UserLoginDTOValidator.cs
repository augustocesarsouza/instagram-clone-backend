using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.UserValidator
{
    public class UserLoginDTOValidator : AbstractValidator<UserDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email do Usuario tem que ser informador para fazer Login");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha deve ser Informada para Confirmar Login do Usuario");
        }
    }
}
