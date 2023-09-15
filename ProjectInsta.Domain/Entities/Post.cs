using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class Post
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public string? PublicId { get; private set; }
        public int IsImagem { get; private set; }
        public int? CounterOfLikes { get; private set; }

        public int? AuthorId { get; private set; }
        public User User { get; private set; }

        public ICollection<Comment> Comments { get; private set; }
        public int? CommentsLikes { get; private set; }
        public ICollection<PostLike> PostLikes { get; private set; }
        public int? PostLikesCounts { get; private set; }

        public Post()
        {
        }

        public Post(int id, string title, string url)
        {
            Id = id;
            Title = title;
            Url = url;
        }

        public Post(int id, string url)
        {
            Id = id;
            Url = url;
        }

        public Post(int id, User user)
        {
            Id = id;
            User = user;
        }

        public Post(int id, string title, string url, string? publicId, int isImagem, int? counterOfLikes, int? authorId) : this(id, title, url)
        {
            PublicId = publicId;
            IsImagem = isImagem;
            CounterOfLikes = counterOfLikes;
            AuthorId = authorId;
        }
        
        public Post(int id, string url, int isImagem) : this(id, url)
        {
            IsImagem = isImagem;
        }

        public Post(int id, string title, string url, User user, int? postLikesCounts, ICollection<PostLike> postLikes) : this(id, title, url)
        {
            User = user;
            PostLikesCounts = postLikesCounts;
            PostLikes = postLikes;
        }

        public Post(int id, string title, string url, string? publicId, int? authorId, int isImagem) : this(id, title, url)
        {
            PublicId = publicId;
            AuthorId = authorId;
            IsImagem = isImagem;
        }

        public Post(string title, string url, string? publicId, int? authorId)
        {
            Validator(title, url, publicId, authorId);
        }

        public Post(int id, string title, string url, User user, int? postLikesCounts, int? commentsLikes, ICollection<PostLike> postLikes) : this(id, title, url)
        {
            User = user;
            PostLikesCounts = postLikesCounts;
            CommentsLikes = commentsLikes;
            PostLikes = postLikes;
        }

        public Post(int id, string title, string url, int isImagem, User user, int? postLikesCounts, int? commentsLikes,
            ICollection<PostLike> postLikes
            ) : this(id, title, url)
        {
            IsImagem = isImagem;
            User = user;
            PostLikesCounts = postLikesCounts;
            CommentsLikes = commentsLikes;
            PostLikes = postLikes;
        }

        public Post(int id, string title, string url, int isImagem, User user, int? postLikesCounts
            ) : this(id, title, url)
        {
            IsImagem = isImagem;
            User = user;
            PostLikesCounts = postLikesCounts;
        }

        public Post(int id, string title, string url, ICollection<Comment> comments) : this(id, title, url)
        {
            Comments = comments;
        }

        public Post(int id, string url, User user, int? postLikesCounts, int? commentsLikes, ICollection<PostLike> postLikes) : this(id, url)
        {
            User = user;
            PostLikes = postLikes;
            CommentsLikes = commentsLikes;
            PostLikesCounts = postLikesCounts;
        }

        public void CountNumberOfLikes()
        {
            if (CounterOfLikes == null)
            {
                CounterOfLikes = 0;
            }
            CounterOfLikes += 1;
            //CounterOfLikes -= 1;
        }

        public void RemoveNumberOfLikes()
        {
            if (CounterOfLikes == null)
            {
                CounterOfLikes = 0;
            }

            if (CounterOfLikes > 0)
            {
                CounterOfLikes -= 1;
            }
        }

        private void Validator(string title, string url, string? publicId, int? authorId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(title), "Deve ser Informado um Title para a publicação");
            DomainValidationException.When(string.IsNullOrEmpty(url), "Deve ser Informado uma URL para a publicação");
            DomainValidationException.When(authorId <= 0, "Um AuthorId o Criador da publicação deve fornecer o ID");

            Title = title;
            Url = url;
            PublicId = publicId;
            AuthorId = authorId;
            Comments = new List<Comment>();
            PostLikes = new List<PostLike>();
        }
    }
}
