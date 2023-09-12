using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class MessageReel
    {
        public int Id { get; private set; }

        public int MessageId { get; private set; }
        public Message Message { get; private set; }

        public int ReelId { get; private set; }
        public Post Reel { get; private set; }

        public DateTime Timestamp { get; private set; }

        public MessageReel(int messageId, int reelId)
        {
            MessageId = messageId;
            ReelId = reelId;
        }

        public void Validator(int messageId, int reelId)
        {
            DomainValidationException.When(messageId <= 0, "dever ser informado um messageId para criar essa ligação");
            DomainValidationException.When(reelId <= 0, "dever ser informado um reelId para criar essa ligação");

            MessageId = messageId;
            ReelId = reelId; 
            Timestamp = DateTime.UtcNow;
        }
    }
}
