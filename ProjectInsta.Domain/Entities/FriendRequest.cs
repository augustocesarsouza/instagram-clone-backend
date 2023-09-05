using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class FriendRequest
    {
        public int Id { get; private set; }

        public int? SenderId { get; private set; }
        public User Sender { get; private set; }

        public int? RecipientId { get; private set; }
        public User Recipient { get; private set; }

        public string Status { get; private set; }
        public DateTime? CreatedAt { get; private set; }

        public FriendRequest()
        {
        }

        public FriendRequest(string status)
        {
            Status = status;
        }

        public FriendRequest(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public FriendRequest(int id, int? senderId, int? recipientId, string status, DateTime? createdAt) : this(id, status)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            CreatedAt = createdAt;
        }

        public FriendRequest(int id, int? senderId, int? recipientId, string status, User sender) : this(id, status)
        {
            SenderId = senderId;
            RecipientId = recipientId;
            Sender = sender;
        }

        public FriendRequest(int senderId, int recipientId, string status)
        {
            Validator(senderId, recipientId, status);

        }

        public void Validator(int senderId, int recipientId, string status)
        {
            DomainValidationException.When(senderId <= 0, "senderId deve ser maior que zero");
            DomainValidationException.When(recipientId <= 0, "recipentId deve ser maior que zero");
            DomainValidationException.When(string.IsNullOrEmpty(status), "não pode ser null ou empty");

            SenderId = senderId;
            RecipientId = recipientId;
            Status = status;
            CreatedAt = DateTime.Now;
        }
    }
}
