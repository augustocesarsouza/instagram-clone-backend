using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class PropertyTextRepository : IPropertyTextRepository
    {
        private readonly ApplicationDbContext _context;

        public PropertyTextRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PropertyText> GetByStoryId(int storyId)
        {
            var storyText = await
                _context
                .PropertyTexts
                .Where(x => x.StoryId == storyId)
                .FirstOrDefaultAsync();

            return storyText;
        }

        public async Task<PropertyText> CreateAsync(PropertyText propertyText)
        {
            await _context.PropertyTexts.AddAsync(propertyText);
            await _context.SaveChangesAsync();
            return propertyText;
        }

        public async Task<PropertyText> DeleteAsync(PropertyText propertyText)
        {
            _context.PropertyTexts.Remove(propertyText);
            await _context.SaveChangesAsync();
            return propertyText;
        }
    }
}
