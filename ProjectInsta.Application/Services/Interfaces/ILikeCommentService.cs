using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface ILikeCommentService
    {
        public Task<ResultService<ICollection<LikeCommentDTO>>> GetAsync();
        public Task<ResultService<ICollection<LikeCommentDTO>>> GetAllLikeCommentId(int commentId);
        public Task<ResultService<LikeCommentDTO>> CreateAsync(LikeCommentDTO likeCommentDTO);
        public Task<ResultService<LikeCommentDTO>> RemoveAsync(int authorId, int commentId);
        public Task RemoveAsyncNoReturn(int commentId);
        public Task RemoveAsyncNoReturnAll(ICollection<CommentDTO> commentDTOs);
    }
}
