using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.UserValidator
{
    public class UserUpdateDTOValidator: AbstractValidator<UserDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.ImagePerfil)
                .NotEmpty()
                .NotNull()
                .WithMessage("Imagem deve ser informado");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email deve ser informado");
        }
    }
}
