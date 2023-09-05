using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class Message
    {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public DateTime Timestamp { get; private set; }

        public int SenderId { get; private set; }
        public User SenderUser { get; private set; }

        public int RecipientId { get; private set; }
        public User RecipientUser { get; private set; }

        public Message(string content, int senderId, int recipientId)
        {
            Validator(content, senderId, recipientId);
        }

        public void CreateTime()
        {
            Timestamp = DateTime.Now;
        }

        private void Validator(string content, int senderId, int recipientId)
        {
            DomainValidationException.When(string.IsNullOrEmpty(content), "Deve ser informado um Conteudo para a Message");
            DomainValidationException.When(senderId <= 0, "Dever ser maior que zero sender");
            DomainValidationException.When(recipientId <= 0, "Dever ser maior que zero recipient");

            Content = content;
            SenderId = senderId;
            RecipientId = recipientId;
            Timestamp = DateTime.Now;
        }
    }
}
