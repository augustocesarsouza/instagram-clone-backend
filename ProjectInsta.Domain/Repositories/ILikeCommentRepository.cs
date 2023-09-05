using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface ILikeCommentRepository
    {
        public Task<ICollection<LikeComment>> GetAsync();
        public Task<LikeComment> GetAuthorIdCommentId(int authorId, int commentId);
        public Task<ICollection<LikeComment>> GetAllLikeCommentId(int commentId);
        public Task<LikeComment> GetByCommentIdAndAuthorId(int commentId, int authorId);
        public Task<ICollection<LikeComment>> GetByCommentId(int? commentId);
        public Task<LikeComment> CreateAsync(LikeComment likeComment);
        public Task<LikeComment> RemoveAsync(LikeComment likeComment);
    }
}
