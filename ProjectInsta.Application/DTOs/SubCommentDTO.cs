namespace ProjectInsta.Application.DTOs
{
    public class SubCommentDTO
    {
        public int? Id { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UserId { get; set; }
        public int? CommentId { get; set; }
        public UserDTO? User { get; set; }
    }
}
