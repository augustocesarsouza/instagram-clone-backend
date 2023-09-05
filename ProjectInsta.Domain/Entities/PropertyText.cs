using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class PropertyText
    {
        public int Id { get; private set; }
        public double Top { get; private set; }
        public double Left { get; private set; }
        public string Text { get; private set; }
        public int Width { get; private set; }
        public int? Height { get; private set; }
        public string Background { get; private set; }
        public string FontFamily { get; private set; }

        public int StoryId { get; private set; }

        //public int StoryIdRefTable { get; private set; }
        public Story Story { get; private set; }

        public PropertyText(int id, double top, double left, string text, int width, int? height, string background, string fontFamily, int storyId)
        {
            Id = id;
            Top = top;
            Left = left;
            Text = text;
            Width = width;
            Height = height;
            Background = background;
            FontFamily = fontFamily;
            StoryId = storyId;
        }

        public void Validator(double top, double left, string text, int width, string background, string fontFamily, int storyId)
        {
            //DomainValidationException.When(top , "posição top dever informada");
            //DomainValidationException.When(left , "posição left dever informada");
            DomainValidationException.When(string.IsNullOrEmpty(text), "erro text dever ser informado");
            DomainValidationException.When(width < 0, "erro width não poder ser negativo");
            DomainValidationException.When(string.IsNullOrEmpty(background), "erro background não poder ser nulo");
            DomainValidationException.When(storyId <= 0, "não poder ser menor igual zero");

            Top = top;
            Left = left;
            Text = text;
            Width = width;
            Background = background;
            //StoryIdRefTable = storyIdRefTable;
            StoryId = storyId;
            FontFamily = fontFamily;
        }
    }
}
