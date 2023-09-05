using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IPostLikeService
    {
        public Task<ResultService<ICollection<PostLikeDTO>>> GetAllAsync();
        public Task<ResultService<PostLikeDTO>> GetByUserIdAndPostId(int userId, int postId);
        public Task<ResultService<ICollection<PostLikeDTO>>> GetByPostIdAsync(int postId);
        public Task<ResultService<PostLikeDTO>> CreateAsync(PostLikeDTO postLikeDTO);
        public Task DeleteAsyncNotReturn(int postId);
        public Task<ResultService<PostLikeDTO>> DeleteAsync(int userId, int postId);
    }
}
