using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly ApplicationDbContext _ctx;

        public StoryRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Story> GetByIdAsync(int id)
        {
            var story = await _ctx
                .Stories
                .Where(x => x.Id == id)
                .Select(x => new Story(x.Id, x.Url, x.PublicId, x.IsImagem))
                .FirstOrDefaultAsync();

            return story;
        }

        public async Task<List<Story>> GetByUserIdAsync(int userCreatePost) 
            // vou chamar 6 video no primeiro GET ordem Com mais likes depois quando a pessoa for scrollando para baixo se chegar no 3 Video pego mais 6 assim por diante 
        {
            var story = await _ctx
                .Stories
                .Where(x => x.AuthorId == userCreatePost)
                .AsNoTracking()
                .Select(x => new Story(x.Id, x.Url, x.CreatedAt, x.IsImagem, x.PropertyText != null ? new PropertyText(
                    x.PropertyText.Id,
                    x.PropertyText.Top,
                    x.PropertyText.Left,
                    x.PropertyText.Text,
                    x.PropertyText.Width,
                    x.PropertyText.Height,
                    x.PropertyText.Background,
                    x.PropertyText.FontFamily,
                    x.PropertyText.StoryId) : new PropertyText(0, 0, 0, "", 0, 0, "", "", 0)))
                .ToListAsync();

            return story;
        }

        public async Task<Story> CreateAsync(Story story)
        {
            await _ctx.Stories.AddAsync(story);
            await _ctx.SaveChangesAsync();
            return story;
        }

        public async Task<Story> UpdateAsync(Story story)
        {
            _ctx.Stories.Update(story);
            await _ctx.SaveChangesAsync();
            return story;
        }

        public async Task<Story> DeleteAsync(Story story)
        {
            _ctx.Stories.Remove(story);
            await _ctx.SaveChangesAsync();
            return story;
        }
    }
}
