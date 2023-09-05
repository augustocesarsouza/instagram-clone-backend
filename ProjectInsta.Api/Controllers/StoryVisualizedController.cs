using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class StoryVisualizedController : ControllerBase
    {
        private readonly IStoryVisualizedService _storyVisualizedService;

        public StoryVisualizedController(IStoryVisualizedService storyVisualizedService)
        {
            _storyVisualizedService = storyVisualizedService;
        }

        [HttpGet("v1/storybyid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _storyVisualizedService.GetById(id);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/storygetuseridstory/{userViewed}/{userCreatePost}")]
        public async Task<IActionResult> GetByUserIdAndStoryId([FromRoute] int userViewed, [FromRoute] int userCreatePost)
        {
            var result = await _storyVisualizedService.GetByUserIdAndStoryId(userViewed, userCreatePost);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/storycreate")]
        public async Task<IActionResult> CreateAsync([FromBody] StoryVisualizedDTO storyVisualizedDTO)
        {
            var result = await _storyVisualizedService.CreateAsync(storyVisualizedDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/storyupdate")]
        public async Task<IActionResult> UpdateAsync([FromBody] StoryVisualizedDTO storyVisualizedDTO)
        {
            var result = await _storyVisualizedService.UpdateAsync(storyVisualizedDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/storydelte")]
        public async Task<IActionResult> DeleteAsync([FromBody] StoryVisualizedDTO storyVisualizedDTO)
        {
            var result = await _storyVisualizedService.DeleteAsync(storyVisualizedDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
