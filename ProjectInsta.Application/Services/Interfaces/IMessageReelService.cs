using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IMessageReelService
    {
        public Task<ResultService<MessageReelDTO>> CreateAsync(MessageReelDTO messageReelDTO);
    }
}
