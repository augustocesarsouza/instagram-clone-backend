using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class PropertyTextController : ControllerBase
    {
        private readonly IPropertyTextService _propertyTextService;

        public PropertyTextController(IPropertyTextService propertyTextService)
        {
            _propertyTextService = propertyTextService;
        }

        [HttpGet("v1/getpropstoryid/{storyId}")]
        public async Task<ActionResult> GetByStoryId(int storyId)
        {
            var story = await _propertyTextService.GetByStoryId(storyId);
            if(story.IsSucess)
                return Ok(story);

            return BadRequest(story);
        }

        [HttpPost("v1/createpropstory/{storyId}")]
        public async Task<ActionResult> CreateAsync([FromBody] PropertyTextDTO propertyTextDTO, [FromRoute] int storyId)
        {
            var story = await _propertyTextService.CreateAsync(propertyTextDTO, storyId);
            if(story.IsSucess)
                return Ok(story);

            return BadRequest(story);
        }
    }
}
