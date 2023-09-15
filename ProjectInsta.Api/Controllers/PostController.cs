using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services;
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

        [HttpGet("v1/post/data/to/message/{postId}")]
        public async Task<IActionResult> GetOnlyNameAndImgUserByPostIdToMessage([FromRoute] int postId)
        {
            var result = await _postService.GetOnlyNameAndImgUserByPostIdToMessage(postId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/post/all")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var result = await _postService.GetAllPostsAsync();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/post/author/{authorId}")]
        public async Task<IActionResult> GetByAuthorId([FromRoute] int authorId)
        {
            var result = await _postService.GetPostByAuthorId(authorId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/post/three/{userId}")]
        public async Task<IActionResult> GetThereePostAsync([FromRoute] int userId)
        {
            var result = await _postService.GetThreeLastPostAsync(userId);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/post/videos/reels")]
        public async Task<IActionResult> GetVideosReels()
        {
            var result = await _postService.GetVideosForReels();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/post/video/info/{reelId}")]
        public async Task<IActionResult> GetVideoToReelInfo([FromRoute] int reelId)
        {
            var result = await _postService.GetVideoToReelInfo(reelId);

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

        [HttpPost("v1/post/create/video/{positionY}")]
        public async Task<IActionResult> CreatePostVideoAsync([FromBody] PostDTO postDTO, [FromRoute] int positionY)
        {
            var result = await _postService.CreatePostVideoAsync(postDTO, positionY);
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
