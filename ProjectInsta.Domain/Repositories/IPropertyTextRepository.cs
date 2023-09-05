using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IPropertyTextRepository
    {
        public Task<PropertyText> GetByStoryId(int storyId);
        public Task<PropertyText> CreateAsync(PropertyText propertyText);
        public Task<PropertyText> DeleteAsync(PropertyText propertyText);
    }
}
