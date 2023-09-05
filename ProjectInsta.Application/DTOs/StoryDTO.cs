using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class StoryDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string? PublicId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? IsImagem { get; set; }

        public int? AuthorId { get; set; }
        public UserDTO? Author { get; set; }

        public PropertyTextDTO? PropertyText { get; set; }
    }
}
