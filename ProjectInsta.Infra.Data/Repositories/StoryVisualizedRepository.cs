using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class StoryVisualizedRepository : IStoryVisualizedRepository
    {
        private readonly ApplicationDbContext _context;

        public StoryVisualizedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StoryVisualized> GetById(int? id)
        {
            var story = await _context
                .StoryVisualizeds
                .FirstOrDefaultAsync(x => x.Id == id);

            return story;
        }

        public async Task<StoryVisualized> GetByUserIdAndStoryId(int? userViewed, int? userCreatePost)
        {
            var storyVisualized = await _context
                .StoryVisualizeds
                .Where(x => x.UserViewedId == userViewed && x.UserCreatedPostId == userCreatePost)
                .OrderByDescending(x => x.Id)
                .Select(x=> new StoryVisualized(x.UserViewedId, x.UserCreatedPostId, x.StoryId))
                .FirstOrDefaultAsync();

            return storyVisualized;
        }

        public async Task<StoryVisualized> GetCheckAlreadyExists(int? userViewed, int? storyId)
        {
            var storyVisualized = await _context
                .StoryVisualizeds
                .Where(x => x.UserViewedId == userViewed && x.StoryId == storyId)
                .Select(x => new StoryVisualized(x.Id))
                .FirstOrDefaultAsync();

            return storyVisualized;
        }

        public async Task<ICollection<StoryVisualized>> GetByStoryIdVisualized(int? storyId)
        {
            var storyVisualized = await _context
                .StoryVisualizeds
                .Where(x => x.StoryId == storyId)
                //.Select(x => new StoryVisualized(x.Id))
                .ToListAsync();

            return storyVisualized;
        }

        public async Task<StoryVisualized> CreateAsync(StoryVisualized storyVisualized)
        {
            await _context.StoryVisualizeds.AddAsync(storyVisualized);
            await _context.SaveChangesAsync();
            return storyVisualized;
        }

        public async Task<StoryVisualized> UpdateAsync(StoryVisualized storyVisualized)
        {
            _context.StoryVisualizeds.Update(storyVisualized);
            await _context.SaveChangesAsync();
            return storyVisualized;
        }

        public async Task<StoryVisualized> DeleteAsync(StoryVisualized storyVisualized)
        {
            _context.StoryVisualizeds.Remove(storyVisualized);
            await _context.SaveChangesAsync();
            return storyVisualized;
        }
    }
}
