using ProjectInsta.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace ProjectInsta.Domain.Entities
{
    public class Comment
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public int? UserId { get; private set; }
        public User? User { get; private set; }

        public int? PostId { get; private set; }
        public Post? Post { get; private set; }
        
        public List<SubComment> SubComments { get; set; } = new();
        public int SubCommentsCounts { get; private set; }
        public List<CommentLike> CommentLikes { get; private set; } = new();
        public List<LikeComment> LikeComments { get; private set; } = new();
        public int LikeCommentsCounts { get; private set; }

        public Comment()
        {
        }

        public Comment(int id, string text, DateTime createdAt)
        {
            Id = id;
            Text = text;
            CreatedAt = createdAt;
            
        }

        public Comment(int? userId, int? postId)
        {
            UserId = userId;
            PostId = postId;
        }

        public Comment(int id, string text, DateTime createdAt, int? postId, User? user) : this(id, text, createdAt)
        {
            PostId = postId;
            User = user;
        }

        public Comment(int id, string text, DateTime createdAt, User? user) : this(id, text, createdAt)
        {
            User = user;
        }

        public Comment(int id, string text, DateTime createdAt, User? user, int subCommentsCounts, int likeCommentsCounts, List<LikeComment> likeComments): this(id, text, createdAt)
        {
            User = user;
            SubCommentsCounts = subCommentsCounts;
            LikeCommentsCounts = likeCommentsCounts;
            LikeComments = likeComments;
        }

        public Comment(int id, List<LikeComment> likeComments, int likeCommentsCounts)
        {
            Id = id;
            LikeComments = likeComments;
            LikeCommentsCounts = likeCommentsCounts;
        }

        public Comment(string text, int? userId, int? postId)
        {
            Validator(text, userId, postId);
        }

        public void Validator(string text, int? userId, int? postId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(text), "Deve ser informado um Text para comentario");
            DomainValidationException.When(userId <= 0, "Deve ser maior que zero");
            DomainValidationException.When(postId <= 0, "Deve ser maior que zero");

            Text = text;
            UserId = userId;
            PostId = postId;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
