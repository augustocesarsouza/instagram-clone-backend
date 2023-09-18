using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet("v1/story/user/all/{idCreator}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int idCreator)
        {
            var result = await _storyService.GetByUserIdAsync(idCreator);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/story")]
        public async Task<IActionResult> CreateAsync([FromBody] StoryDTO storyDTO)
        {
            var result = await _storyService.CreateAsync(storyDTO);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/story")]
        public async Task<IActionResult> UpdateAsync([FromBody] StoryDTO storyDTO)
        {
            var result = await _storyService.UpdateAsync(storyDTO);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/story/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = await _storyService.DeleteAsync(id);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
