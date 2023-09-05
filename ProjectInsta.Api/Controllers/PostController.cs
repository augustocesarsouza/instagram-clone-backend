﻿using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var result = await _postService.GetAllPostsAsync();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/postAuthorId/{authorId}")]
        public async Task<IActionResult> GetByAuthorId([FromRoute] int authorId)
        {
            var result = await _postService.GetPostByAuthorId(authorId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/threePosts/{userId}")]
        public async Task<IActionResult> GetThereePostAsync([FromRoute] int userId)
        {
            var result = await _postService.GetThreeLastPostAsync(userId);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/videosreels")]
        public async Task<IActionResult> GetVideosReels()
        {
            var result = await _postService.GetVideosForReels();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/countpostlike/{postId}")]
        public async Task<IActionResult> CountPostLike([FromRoute] int postId)
        {
            var result = await _postService.CountPostLike(postId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/removelikecount/{postId}")]
        public async Task<IActionResult> RemoveLikeCount([FromRoute] int postId)
        {
            var result = await _postService.RemoveLikeCount(postId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/post")]
        public async Task<IActionResult> CreatePostAsync([FromBody] PostDTO postDTO)
        {
            var result = await _postService.CreatePostAsync(postDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/post/{id}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}