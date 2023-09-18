using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class CreateImgProcessController : ControllerBase
    {
        private readonly ICreateImgProcess _createImgProcess;

        public CreateImgProcessController(ICreateImgProcess createImgProcess)
        {
            _createImgProcess = createImgProcess;
        }

        [HttpPost("v1/process/img/story")]
        public async Task<IActionResult> ProcessImgStory([FromBody] ProcessImgDTO processImgDTO)
        {
            var result = await _createImgProcess.ProcessImgStory(processImgDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/process/img/framevideo/post")]
        public async Task<IActionResult> ProcessImgCreateToProfileVideos([FromBody] ProcessImgDTO processImgDTO)
        {
            var result = await _createImgProcess.ProcessImgCreateToProfileVideos(processImgDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/deleteimg")]
        public async Task<IActionResult> DeleteImg([FromBody] ProcessImgDTO processImgDTO)
        {
            var result = await _createImgProcess.DeleteImgCloudinary(processImgDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
