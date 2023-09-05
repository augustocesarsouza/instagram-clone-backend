using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly ApplicationDbContext _ctx;

        public FriendRequestRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<FriendRequest?> GetSenderAndRecipientAsync(int? senderId, int? recipientId)
        {
            var friend = await _ctx.FriendRequests
                .Where(x => x.SenderId == senderId && x.RecipientId == recipientId || x.SenderId == recipientId && x.RecipientId == senderId)
                .Select(x => new FriendRequest(x.Id, x.SenderId, x.RecipientId, x.Status, x.CreatedAt))
                .FirstOrDefaultAsync();

            return friend;
        }

        public async Task<List<FriendRequest>> GetCheckRequestsFriendship(int recipientId)
        {
            var friend = await _ctx.FriendRequests
                .Include(x => x.Sender)
                .Where(x => x.RecipientId == recipientId && x.Status == "Pending")
                .Select(x => new FriendRequest(x.Id, x.SenderId, x.RecipientId, x.Status, new User(x.Sender.Id, x.Sender.Name, x.Sender.ImagePerfil)))
                .ToListAsync();

            return friend;
        }

        public async Task<FriendRequest> CreateAsync(FriendRequest friendRequest)
        {
            await _ctx.FriendRequests.AddAsync(friendRequest);
            await _ctx.SaveChangesAsync();
            return friendRequest;
        }

        public async Task<FriendRequest> UpdateAsync(FriendRequest friendRequest)
        {
            _ctx.FriendRequests.Update(friendRequest);
            await _ctx.SaveChangesAsync();
            return friendRequest;
        }

        public async Task<FriendRequest> DeleteAsync(FriendRequest friendRequest)
        {
            _ctx.FriendRequests.Remove(friendRequest);
            await _ctx.SaveChangesAsync();
            return friendRequest;
        }
    }
}
