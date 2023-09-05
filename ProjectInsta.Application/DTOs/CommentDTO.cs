using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class CommentDTO
    {
        public int? Id { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public UserDTO? User { get; set; }
        public int? SubCommentsCounts { get; set; }
        public int? LikeCommentsCounts { get; set; }
        public List<LikeCommentDTO> LikeComments { get; set; } = new();
    }
}
