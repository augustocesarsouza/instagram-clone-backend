using ProjectInsta.Application.DTOs;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IStoryVisualizedService
    {
        public Task<ResultService<StoryVisualizedDTO>> GetById(int id);
        public Task<ResultService<StoryVisualizedDTO>> GetByUserIdAndStoryId(int? userViewed, int? userCreatePost);
        public Task<ResultService<ICollection<StoryVisualizedDTO>>> GetByStoryIdVisualized(int? storyId);
        public Task<ResultService<StoryVisualizedDTO>> CreateAsync(StoryVisualizedDTO storyVisualized);
        public Task<ResultService<StoryVisualizedDTO>> UpdateAsync(StoryVisualizedDTO storyVisualized);
        public Task<ResultService<StoryVisualizedDTO>> DeleteAsync(StoryVisualizedDTO storyVisualized);
    }
}
