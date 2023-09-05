using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class SubComment
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public int UserId { get; private set; }
        public User User { get; private set; }

        public int CommentId { get; private set; }
        public Comment Comment { get; private set; }

        public SubComment()
        {
        }

        public SubComment(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public SubComment(int id, string text, int commentId, User user) : this(id, text)
        {
            CommentId = commentId;
            User = user;
        }

        public SubComment(string text, int userId, int commentId)
        {
            Validator(text, userId, commentId);
        }

        private void Validator(string text, int userId, int commentId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(text), "Texto do subComment deve ser informado");
            DomainValidationException.When(userId <= 0, "userId deve ser maior que zero");
            DomainValidationException.When(commentId <= 0, "commentId deve ser maior que zero");

            Text = text;
            UserId = userId;
            CommentId = commentId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
