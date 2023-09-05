using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class StoryVisualizedDTO
    {
        public int? Id { get; set; }

        public int? UserViewedId { get; set; }
        public UserDTO? UserViewed { get; set; }

        public int? UserCreatedPostId { get; set; }
        public UserDTO? UserCreatedPost { get; set; }

        public int? StoryId { get; set; }
        public StoryDTO? Story { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
