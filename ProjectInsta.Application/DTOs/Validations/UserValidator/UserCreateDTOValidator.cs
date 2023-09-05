using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.UserValidator
{
    public class UserCreateDTOValidator : AbstractValidator<UserDTO>
    {
        public UserCreateDTOValidator()
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
