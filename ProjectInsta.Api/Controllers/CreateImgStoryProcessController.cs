using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class CreateImgStoryProcessController : ControllerBase
    {
        private readonly ICreateImgStoryProcess _createImgStoryProcess;

        public CreateImgStoryProcessController(ICreateImgStoryProcess createImgStoryProcess)
        {
            _createImgStoryProcess = createImgStoryProcess;
        }

        [HttpPost("v1/processimg")]
        public async Task<IActionResult> ProcessImg([FromBody] ProcessImgDTO processImgDTO)
        {
            var result = await _createImgStoryProcess.ProcessImg(processImgDTO);
            if (result.IsSucess)
                return Ok(result);
            
            return BadRequest(result);
        }

        [HttpDelete("v1/deleteimg")]
        public async Task<IActionResult> DeleteImg([FromBody] ProcessImgDTO processImgDTO)
        {
            var result = await _createImgStoryProcess.DeleteImgCloudinary(processImgDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
