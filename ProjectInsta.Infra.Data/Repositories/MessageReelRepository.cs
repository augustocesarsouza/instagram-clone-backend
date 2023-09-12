using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class MessageReelRepository : IMessageReelRepository
    {
        private readonly ApplicationDbContext _ctx;

        public MessageReelRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<MessageReel> CreateAsync(MessageReel messageReel)
        {
            await _ctx.MessageReels.AddAsync(messageReel);
            await _ctx.SaveChangesAsync();
            return messageReel;
        }
    }
}
