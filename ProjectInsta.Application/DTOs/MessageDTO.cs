using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class MessageDTO
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public int? SenderId { get; set; }
        public int? RecipientId { get; set; }

        public int? ReelId { get; set; }

        public string? UrlFrameReel { get; set; }
        public string? PublicIdFrameReel { get; set; }

        public DateTime? Timestamp { get; set; } = DateTime.Now;
    }
}
