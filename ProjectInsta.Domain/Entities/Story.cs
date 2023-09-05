using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class Story
    {
        public int Id { get; private set; }
        public string Url { get; private set; }
        public string PublicId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int? IsImagem { get; private set; }

        //public List<StoryVisualized> StoryVisualizeds { get; private set; }
        public PropertyText? PropertyText { get; private set; }

        public int AuthorId { get; private set; }
        public User Author { get; private set; }

        public Story()
        {
        }

        public Story(int id, string url)
        {
            Id = id;
            Url = url;
        }

        public Story(int id, string url, string publicId, int? isImagem) : this(id, url)
        {
            PublicId = publicId;
            IsImagem = isImagem;
        }

        public Story(int id, string url, DateTime createdAt, int? isImagem, PropertyText propertyText) : this(id, url)
        {
            CreatedAt = createdAt;
            IsImagem = isImagem;
            PropertyText = propertyText;
        }

        public Story(string url, string publicId, int authorId)
        {
            Validator(url, publicId, authorId);
        }

        public void IsImagemMethod(int id)
        {
            IsImagem = id;
        }

        public void Validator(string url, string publicId, int authorId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(url), "Url deve ser informada");
            DomainValidationException.When(string.IsNullOrEmpty(publicId), "Deve ser informado publicId");
            DomainValidationException.When(authorId <= 0, "id do author deve ser maior que 0");

            Url = url;
            PublicId = publicId;
            AuthorId = authorId;
            CreatedAt = DateTime.Now;
        }
    }
}
