using FluentValidation;

namespace ProjectInsta.Application.DTOs.Validations.PropertyText
{
    public class PropertyTextDTOValidator : AbstractValidator<PropertyTextDTO>
    {
        public PropertyTextDTOValidator()
        {
            RuleFor(x => x.Top)
                //.GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Posição top não pode ser nulo");

            RuleFor(x => x.Left)
                //.GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Posição Left não pode ser nulo");

            RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty()
                .WithMessage("Text tem que ser informado!");

            RuleFor(x => x.Width)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Valor width não poder ser menor que zero");

            RuleFor(x => x.Background)
                .NotNull()
                .NotEmpty()
                .WithMessage("Valor do background deve ser informado!");

            RuleFor(x => x.FontFamily)
                .NotNull()
                .NotEmpty()
                .WithMessage("Valor do FontFamily deve ser informado!");

            RuleFor(x => x.StoryId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("StoryId de ser informado!");

            //RuleFor(x => x.StoryIdRefTable)
            //    .GreaterThan(0)
            //    .NotNull()
            //    .NotEmpty()
            //    .WithMessage("StoryIdRefTable de ser informado!");

            RuleFor(x => x.StoryId)
               .GreaterThan(0)
               .NotNull()
               .NotEmpty()
               .WithMessage("StoryId de ser informado!");
        }
    }
}
