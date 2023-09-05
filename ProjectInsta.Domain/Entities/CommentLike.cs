using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class CommentLike
    {
        public int Id { get; private set; }

        public int CommentId { get; private set; }
        public Comment Comment { get; private set; }
        public int AuthorId { get; private set; }
        public User User { get; private set; }

        public CommentLike(int commentId, int authorId)
        {
            Validator(commentId, authorId);
        }

        public void Validator(int commentId, int authorId)
        {
            DomainValidationException.When(commentId <= 0, "commentId deve ser maior que zero");
            DomainValidationException.When(authorId <= 0, "authorId deve ser maior que zero");

            CommentId = commentId;
            AuthorId = authorId;
        }
    }
}
