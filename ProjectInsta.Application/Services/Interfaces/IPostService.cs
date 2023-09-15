﻿using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IPostService
    {
        public Task<ResultService<PostDTO>> GetOnlyNameAndImgUserByPostIdToMessage(int postId);
        public Task<ResultService<ICollection<PostDTO>>> GetAllPostsAsync();
        public Task<ResultService<ICollection<PostDTO>>> GetPostByAuthorId(int authorId);
        public Task<ResultService<ICollection<PostDTO>>> GetThreeLastPostAsync(int userId);
        public Task<ResultService<ICollection<PostDTO>>> GetVideosForReels();
        public Task<ResultService<PostDTO>> GetVideoToReelInfo(int reelId);
        public Task<ResultService<PostDTO>> CreatePostAsync(PostDTO postDTO);
        public Task<ResultService<PostDTO>> CreatePostVideoAsync(PostDTO postDTO, int positionY);
        public Task<ResultService<PostDTO>> DeletePostAsync(int id);
    }
}
