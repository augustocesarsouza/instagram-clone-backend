using ProjectInsta.Application.DTOs;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IFriendRequestService
    {
        public Task<ResultService<FriendRequestDTO>> GetSenderAndRecipientAsync(int? senderId, int? recipientId);
        public Task<ResultService<List<FriendRequestDTO>>> GetCheckRequestsFriendship(int recipientId);
        public Task<ResultService<FriendRequestDTO>> CreateAsync(FriendRequestDTO friendRequest);
        public Task<ResultService<FriendRequestDTO>> UpdateAsync(FriendRequestDTO friendRequest);
        public Task<ResultService<FriendRequestDTO>> DeleteAsync(int senderId, int recipientId);
    }
}
