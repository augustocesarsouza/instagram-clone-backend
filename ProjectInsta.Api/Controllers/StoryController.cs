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

        [HttpGet("v1/storyall")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _storyService.GetAllStory();
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/storyallbyuserCreatePost/{userCreatePost}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int userCreatePost)
        {
            var result = await _storyService.GetByUserIdAsync(userCreatePost);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/storyupdateuservisualizedstory/{storyId}/{idUserView}")]
        public async Task<IActionResult> UpdateUserVisualizedStory([FromRoute] int storyId, [FromRoute] int idUserView)
        {
            var result = await _storyService.UpdateUserVisualizedStory(storyId, idUserView);
            if (result.IsSucess)
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
