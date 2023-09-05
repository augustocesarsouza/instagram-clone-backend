using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class LikeCommentController : ControllerBase
    {
        private readonly ILikeCommentService _likeCommentService;

        public LikeCommentController(ILikeCommentService likeCommentService)
        {
            _likeCommentService = likeCommentService;
        }

        [HttpGet("v1/likeComments")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _likeCommentService.GetAsync();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/likeCommentId/{commentId}")] //Posso excluir esse get aqui
        public async Task<IActionResult> GetLikeCommentId([FromRoute] int commentId)
        {
            var result = await _likeCommentService.GetAllLikeCommentId(commentId);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/likeCommentCreate")]
        public async Task<IActionResult> CreateAsync([FromBody] LikeCommentDTO likeCommentDTO)
        {
            var result = await _likeCommentService.CreateAsync(likeCommentDTO);
            if (result.IsSucess)
                return Ok(result);

            if (result.IsSucessDeleteLike)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/likeCommentDelete/{authorId}/{commentId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int authorId, [FromRoute] int commentId)
        {
            var result = await _likeCommentService.RemoveAsync(authorId, commentId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
