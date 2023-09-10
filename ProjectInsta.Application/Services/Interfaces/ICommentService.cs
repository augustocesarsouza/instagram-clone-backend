using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<ResultService<ICollection<CommentDTO>>> GetCommentsByPostIdAsync(int postId);
        public Task<ResultService<ICollection<CommentDTO>>> GetLikeCommentsInfo(int postId);
        public Task<ResultService<ICollection<CommentDTO>>> GetByPostIdAsyncForReels(int postId);
        public Task<ResultService<CommentDTO>> CreateCommentAsync(CommentDTO commentDTO);
        public Task<ResultService<CommentDTO>> DeleteByCommentIdAsync(int commentId);
        public Task DeleteAsyncNotReturn(int? postId);
    }
}
