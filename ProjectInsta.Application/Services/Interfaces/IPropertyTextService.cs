using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IPropertyTextService
    {
        public Task<ResultService<PropertyTextDTO>> GetByStoryId(int storyId);
        public Task<ResultService<PropertyTextDTO>> CreateAsync(PropertyTextDTO propertyTextDTO, int storyId);
        public Task<ResultService<PropertyTextDTO>> DeleteAsync(int storyId);
    }
}
