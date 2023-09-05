using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IFriendRequestRepository
    {
        public Task<FriendRequest?> GetSenderAndRecipientAsync(int? senderId, int? recipientId);
        public Task<List<FriendRequest>> GetCheckRequestsFriendship(int recipientId);
        public Task<FriendRequest> CreateAsync(FriendRequest friendRequest);
        public Task<FriendRequest> UpdateAsync(FriendRequest friendRequest);
        public Task<FriendRequest> DeleteAsync(FriendRequest friendRequest);
    }
}
