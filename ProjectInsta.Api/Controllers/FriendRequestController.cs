using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;

        public FriendRequestController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }

        [HttpGet("v1/friendrequest/{senderId}/{recipientId}")]
        public async Task<IActionResult> GetSenderAndRecipientAsync([FromRoute] int senderId, [FromRoute] int recipientId)
        {
            var result = await _friendRequestService.GetSenderAndRecipientAsync(senderId, recipientId);

            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/checkrequestfriendship/{recipientId}")]
        public async Task<IActionResult> GetCheckRequestsFriendship([FromRoute] int recipientId)
        {
            var result = await _friendRequestService.GetCheckRequestsFriendship(recipientId);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/friendrequest")]
        public async Task<IActionResult> CreateAsync([FromBody] FriendRequestDTO friendRequestDTO)
        {
            var result = await _friendRequestService.CreateAsync(friendRequestDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/friendrequest")]
        public async Task<IActionResult> UpdateAsync([FromBody] FriendRequestDTO friendRequestDTO)
        {
            var result = await _friendRequestService.UpdateAsync(friendRequestDTO);

            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/friendrequest/{senderId}/{recipientId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int senderId, [FromRoute] int recipientId)
        {
            var result = await _friendRequestService.DeleteAsync(senderId, recipientId);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
