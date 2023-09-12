using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class MessageReelController : ControllerBase
    {
        private readonly IMessageReelService _messageReelService;

        public MessageReelController(IMessageReelService messageReelService)
        {
            _messageReelService = messageReelService;
        }

        [HttpPost("v1/messagereel")]
        public async Task<IActionResult> CreateAsync([FromRoute] MessageReelDTO messageReelDTO)
        {
            var result = await _messageReelService.CreateAsync(messageReelDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
