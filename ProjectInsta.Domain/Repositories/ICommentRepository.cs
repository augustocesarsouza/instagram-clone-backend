using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface ICommentRepository
    {
        public Task<ICollection<Comment>> GetByPostIdAsync(int postId);
        public Task<Comment> GetByCommentIdAsync(int commentId);
        public Task<ICollection<Comment>> GetLikeCommentsInfo(int postId);
        public Task<ICollection<Comment>> GetByPostIdAsyncForReels(int postId);
        public Task<Comment> GetByPostIdOneAsync(int? postId);
        public Task<Comment> GetByUserIdAndPostId(int? userId, int? postId);
        public Task<ICollection<Comment>> GetByPostIdAll(int? postId);
        public Task<Comment> CreateCommentAsync(Comment comment);
        public Task<Comment> DeleteAsync(Comment comment);

    }
}
