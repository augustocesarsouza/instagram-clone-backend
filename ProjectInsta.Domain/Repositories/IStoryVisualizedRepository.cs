using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IStoryVisualizedRepository
    {
        public Task<StoryVisualized> GetById(int? id);
        public Task<StoryVisualized> GetByUserIdAndStoryId(int? userId, int? storyId);
        public Task<StoryVisualized> GetCheckAlreadyExists(int? userViewed, int? storyId);
        public Task<ICollection<StoryVisualized>> GetByStoryIdVisualized(int? storyId);
        public Task<StoryVisualized> CreateAsync(StoryVisualized storyVisualized);
        public Task<StoryVisualized> UpdateAsync(StoryVisualized storyVisualized);
        public Task<StoryVisualized> DeleteAsync(StoryVisualized storyVisualized);
    }
}
