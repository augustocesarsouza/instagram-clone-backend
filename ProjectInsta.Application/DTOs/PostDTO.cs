using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        //public string? Url64 { get; set; }
        public string? PublicId { get; set; }
        public int? IsImagem { get; set; }
        public int? CounterOfLikes { get; private set; }

        public int? AuthorId { get; set; }
        
        public UserDTO? User { get; set; }
        public ICollection<PostLikeDTO>? PostLikes { get; set; }
        public int? PostLikesCounts { get; set; }
        public ICollection<CommentDTO>? Comments { get; set; }
        public int? CommentsLikes { get; set; }
    }
}
