using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class PostLikeController : ControllerBase
    {
        private readonly IPostLikeService _postLikeService;

        public PostLikeController(IPostLikeService postLikeService)
        {
            _postLikeService = postLikeService;
        }

        [HttpGet("v1/postLikes")]
        public async Task<ActionResult> GetAllAsync()
        {
            var result = await _postLikeService.GetAllAsync();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/postLike/{userId}/{postId}")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] int userId, [FromRoute] int postId)
        {
            var result = await _postLikeService.GetByUserIdAndPostId(userId, postId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/postLikes/{postId}")]
        public async Task<ActionResult> GetByPostIdAsync([FromRoute] int postId)
        {
            var result = await _postLikeService.GetByPostIdAsync(postId);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/postLike")]
        public async Task<ActionResult> CreateAsync([FromBody] PostLikeDTO postLikeDTO)
        {
            var result = await _postLikeService.CreateAsync(postLikeDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/postLike/{userId}/{postId}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int userId, [FromRoute] int postId)
        {
            var result = await _postLikeService.DeleteAsync(userId, postId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
