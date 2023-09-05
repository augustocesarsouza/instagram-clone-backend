using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IPostLikeRepository
    {
        public Task<ICollection<PostLike>> GetAllAsync();
        public Task<PostLike?> GetByUserIdAndPostId(int userId, int postId);
        public Task<ICollection<PostLike?>> GetByPostIdAll(int postId);
        public Task<ICollection<PostLike>> GetByPostId(int postId);
        public Task<PostLike> CreateAsync(PostLike postLike);
        public Task<PostLike> DeleteAsync(PostLike postLike);
    }
}
