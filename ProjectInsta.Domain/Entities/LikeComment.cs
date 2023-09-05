using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class LikeComment
    {
        public int Id { get; private set; }

        public int CommentId { get; private set; }
        public Comment Comment { get; private set; }
        public int AuthorId { get; private set; }
        public User Author { get; private set; }

        public LikeComment()
        { 
        }

        public LikeComment(int commentId, int authorId)
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
