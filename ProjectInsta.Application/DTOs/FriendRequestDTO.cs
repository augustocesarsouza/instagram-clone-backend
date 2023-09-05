namespace ProjectInsta.Application.DTOs
{
    public class FriendRequestDTO
    {
        public int? Id { get; set; }

        public int? SenderId { get; set; }
        public UserDTO? Sender { get; set; } 

        public int? RecipientId { get; set; }
        public UserDTO? Recipient { get; set; }
        
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
