using FluentValidation;
using ProjectInsta.Application.DTOs.UserDTOsReturn;

namespace ProjectInsta.Application.DTOs.Validations.UserValidator
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome do Usario Obrigatorio");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email do Usario Obrigatorio");

            RuleFor(x => x.ImagePerfil)
                .NotEmpty()
                .NotNull()
                .WithMessage("Imagem de Perfil do Usario Obrigatorio");

            RuleFor(x => x.BirthDateString)
                .NotEmpty()
                .NotNull()
                .WithMessage("Data de Nasciento Obrigatorio do Usario Obrigatorio");
        }
    }
}
