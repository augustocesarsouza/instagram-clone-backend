namespace ProjectInsta.Application.DTOs
{
    public class SignalRMessageDTO
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }

        public string SenderEmail { get; set; }
        public string RecipientEmail { get; set; }

        public string Content { get; set; }
        //public DateTime Timestamp { get; set; }
    }
}
