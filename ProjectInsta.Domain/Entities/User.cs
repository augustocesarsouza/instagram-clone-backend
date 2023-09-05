using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public string ImagePerfil { get; private set; }
        public string? PublicId { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public DateTime? LastDisconnectedTime { get; private set; }

        public string? Password { get; private set; }
        public string? Token { get; private set; }

        public ICollection<UserPermission> UserPermissions { get; private set; } = new List<UserPermission>();
        public ICollection<Post> Posts { get; private set; } = new List<Post>();
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
        public ICollection<SubComment> SubComments { get; private set; } = new List<SubComment>();
        public ICollection<CommentLike> CommentLikes { get; private set; } = new List<CommentLike>();
        public ICollection<PostLike> PostLikes { get; private set; } = new List<PostLike>();

        public List<Follow> Followers { get; private set; } = new List<Follow>();
        public List<Follow> Following { get; private set; } = new List<Follow>();


        public ICollection<Message> SenderMessage { get; private set; } = new List<Message>();
        public ICollection<Message> RecipientMessage { get; private set; } = new List<Message>();

        public List<LikeComment> LikeComments { get; private set; } = new();

        public User(int id, string name, string imagePerfil)
        {
            Id = id;
            Name = name;
            ImagePerfil = imagePerfil;
        }

        public User(int id, string name, string email, string imagePerfil)
        {
            Id = id;
            Name = name;
            Email = email;
            ImagePerfil = imagePerfil;
        }

        public User(int id, string name, string email, string? passwordHash, string? token)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Token = token;
        }

        public User(string name, string email, string passwordHash, string imagePerfil, string publicId, DateTime? birthDate)
        {
            Validator(name, email, passwordHash, imagePerfil, publicId, birthDate);
        }

        public User(int id, string name, string email, string passwordHash, string imagePerfil, string publicId, DateTime? birthDate)
        {
            DomainValidationException.When(id <= 0, "ID deve ser maior que zero");
            Id = id;
            Validator(name, email, passwordHash, imagePerfil, publicId, birthDate);
        }

        public User(int id, string name, string email, DateTime? lastDisconnectedTime)
        {
            Id = id;
            Name = name;
            Email = email;
            LastDisconnectedTime = lastDisconnectedTime;
        }

        public User(int id, string name, string email, string imagePerfil, DateTime? lastDisconnectedTime) : this(id, name, email, imagePerfil)
        {
            LastDisconnectedTime = lastDisconnectedTime;
        }

        public void Validator(string name, string email, string passwordHash, string imagePerfil, string publicId, DateTime? birthDate)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "Nome do Usuario deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(email), "Email do Usuario deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(passwordHash), "PasswordHash do Usuario deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(imagePerfil), "ImagePerfil do Usuario deve ser informado!");
            DomainValidationException.When(string.IsNullOrEmpty(publicId), "PublicId do Usuario deve ser informado!");
            DomainValidationException.When(!birthDate.HasValue, "BirthDate do Usuario deve ser informado!");

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            ImagePerfil = imagePerfil;
            PublicId = publicId;
            BirthDate = birthDate;
        }

        public void ValidatorToken(string token)
        {
            DomainValidationException.When(string.IsNullOrEmpty(token), "Token não foi Gerado!");
            Token = token;
        }

        public void ValidateLastDisconnectedTime(DateTime dateTime)
        {
            LastDisconnectedTime = dateTime;
        }

        public void ValidateNewImg(string imagePerfil, string publicId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(imagePerfil), "Imagem do usuario obrigatorio");
            DomainValidationException.When(string.IsNullOrEmpty(publicId), "publicId deve ser informado");
            ImagePerfil = imagePerfil;
            PublicId = publicId;
        }
    }
}
