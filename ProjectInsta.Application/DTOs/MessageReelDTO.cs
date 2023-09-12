namespace ProjectInsta.Application.DTOs
{
    public class MessageReelDTO
    {
        public int Id { get; private set; }

        public int MessageId { get; private set; }
        public MessageDTO? Message { get; private set; }

        public int ReelId { get; private set; }
        public PostDTO? Reel { get; private set; }

        public DateTime? Timestamp { get; private set; } = DateTime.UtcNow;
    }
}
