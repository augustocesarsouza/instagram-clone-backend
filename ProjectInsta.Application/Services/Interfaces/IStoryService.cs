using ProjectInsta.Application.DTOs;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IStoryService
    {
        public Task<ResultService<List<StoryDTO>>> GetByUserIdAsync(int userCreatePost);
        public Task<ResultService<StoryDTO>> CreateAsync(StoryDTO story);
        public Task<ResultService<StoryDTO>> UpdateAsync(StoryDTO story);
        public Task<ResultService<StoryDTO>> DeleteAsync(int id);
    }
}
