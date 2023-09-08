using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.ImagemBase64ProfileUserValidator
{
    public class ImagemBase64ProfileUserDTOValidator :AbstractValidator<ImagemBase64ProfileUserDTO>
    {
        public ImagemBase64ProfileUserDTOValidator()
        {
            RuleFor(x => x.Base64)
                .NotEmpty()
                .NotNull()
                .WithMessage("Imagem Base64 deve ser fornecida!");
        }
    }
}
