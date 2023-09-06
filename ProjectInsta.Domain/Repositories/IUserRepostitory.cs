using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IUserRepostitory
    {
        public Task<User?> GetUserDataOnly(int idUser);
        public Task<User?> GetByEmailIsOnlineAsync(string email);
        public Task<List<User>> GetFollowersUser(int userId);
        public Task<HashSet<User>> GetSuggestionForYouProfile(int idFollowing, int idUser);
        public Task<List<User?>> GetUsersFollowignByIdAsync(int idUser);
        public Task<User?> GetByEmail(string email);
        public Task<User?> GetByEmailSignalR(string email);
        public Task<User?> GetByEmailDisconnected(string email);
        public Task<User> CreateAsync(User user);
        public Task<User> UpdateAsync(User user);
    }
}
