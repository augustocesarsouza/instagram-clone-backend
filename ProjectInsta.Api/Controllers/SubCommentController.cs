using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class SubCommentController : ControllerBase
    {
        private readonly ISubCommentService _subCommentService;

        public SubCommentController(ISubCommentService subCommentService)
        {
            _subCommentService = subCommentService;
        }

        [HttpGet("v1/subComment/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var subComment = await _subCommentService.GetByIdAsync(id);
            if (subComment.IsSucess)
                return Ok(subComment);

            return BadRequest(subComment);
        }

        [HttpGet("v1/subComments")]
        public async Task<IActionResult> GetAllAsync()
        {
            var subComments = await _subCommentService.GetAllAsync();
            if (subComments.IsSucess)
                return Ok(subComments);

            return BadRequest(subComments);
        }

        [HttpGet("v1/subComments/{commentId}/{pagina}/{registroPorPagina}")]
        public async Task<IActionResult> GetCommentIdAsync([FromRoute] int commentId, [FromRoute] int pagina, [FromRoute] int registroPorPagina)
        {
            var subComments = await _subCommentService.GetByCommentIdAsync(commentId, pagina, registroPorPagina);
            if (subComments.IsSucess)
                return Ok(subComments);

            return BadRequest(subComments);
        }

        [HttpPost("v1/subComment")]
        public async Task<IActionResult> CreateAsync([FromBody] SubCommentDTO subCommentDTO)
        {
            try
            {
                var subComment = await _subCommentService.CreateAsync(subCommentDTO);
                if (subComment.IsSucess)
                    return Ok(subComment);

                return BadRequest(subComment);

            }
            catch (DomainValidationException ex)
            {
                var result = ResultService.Fail(ex.Message);
                return BadRequest(result);
            }
        }
    }
}
