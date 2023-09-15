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

        [HttpPost("v1/process/img/framevideo")]
        public async Task<IActionResult> ProcessImgFrameVideoReel([FromBody] ProcessImgDTO processImgDTO)
        {
            var result = await _createImgProcess.ProcessImgCreateFrameReelToMessage(processImgDTO);
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
