using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IPostRepository
    {
        public Task<ICollection<Post>> GetAllPostAsync();
        public Task<Post> GetPostCreate(int postId);
        public Task<Post> GetByIdAsync(int id);
        public Task<Post> GetCheckUserPost(int authorId);
        public Task<ICollection<Post>> GetThreeLastPostAsync(int userId);
        public Task<ICollection<Post>> GetPostByAythorIdAsync(int authorId);
        public Task<Post> GetPostIdOnlyCount(int postId);
        public Task<ICollection<Post>> GetVideosForReels();
        public Task<Post> CreatePostAsync(Post post);
        public Task<Post> UpdatePostAsync(Post post);
        public Task<Post> DeletePostAsync(Post post);
    }
}
