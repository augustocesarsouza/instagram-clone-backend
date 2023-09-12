using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IMessageReelRepository
    {
        public Task<MessageReel> CreateAsync(MessageReel messageReel);
    }
}
