using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IFollowRepository
    {
        public Task<Follow> GetByFollowerAndFollowingAsync(int? followerId, int? followingId);
        public Task<ICollection<Follow>> GetAllFollowingFromUser(int idUser);
        public Task<ICollection<Follow>> GetAllFollowersFromUser(int userId);
        public Task<Follow> CreateAsync(Follow follow);
        public Task<Follow> DeleteAsync(Follow follow);
    }
}
