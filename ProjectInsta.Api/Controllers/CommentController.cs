using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("v1/comments")]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            if(comments.IsSucess)
                return Ok(comments);

            return BadRequest(comments);
        }

        [HttpGet("v1/comments/{postId}")]
        public async Task<IActionResult> GetByPostIdAsync(
            [FromRoute] int postId)
        {
            var comments = await _commentService.GetCommentsByPostIdAsync(postId);
            if (comments.IsSucess)
                return Ok(comments);

            return BadRequest(comments);
        }

        [HttpGet("v1/commentslikeinfo/{postId}")]
        public async Task<IActionResult> GetLikeCommentsInfo(
            [FromRoute] int postId)
        {
            var comments = await _commentService.GetLikeCommentsInfo(postId);
            if (comments.IsSucess)
                return Ok(comments);

            return BadRequest(comments);
        }

        [HttpGet("v1/commentsforreels/{postId}")]
        public async Task<IActionResult> GetByPostIdForReelsAsync(
            [FromRoute] int postId)
        {
            var comments = await _commentService.GetByPostIdAsyncForReels(postId);
            if (comments.IsSucess)
                return Ok(comments);

            return BadRequest(comments);
        }

        [HttpPost("v1/comment")]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CommentDTO commentDTO)
        {
            var comment = await _commentService.CreateCommentAsync(commentDTO);
            if (comment.IsSucess)
                return Ok(comment);

            return BadRequest(comment);
        }

        [HttpDelete("v1/comment/{commentId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int commentId)
        {
            var comment = await _commentService.DeleteByCommentIdAsync(commentId);
            if (comment.IsSucess)
                return Ok(comment);

            return BadRequest(comment);
        }
    }
}
