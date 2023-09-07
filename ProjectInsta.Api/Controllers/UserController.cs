using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("v1/userDataOnly/{idUser}")]
        public async Task<IActionResult> GetUserDataOnly([FromRoute] int idUser)
        {
            var result = await _userService.GetUserDataOnly(idUser);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/followersfromuser/{idUser}")]
        public async Task<IActionResult> GetFollowersUser([FromRoute] int idUser)
        {
            var result = await _userService.GetFollowersUser(idUser);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/get/suggestion/followers/{idFollowing}/{idUser}/{isProfile}")]
        public async Task<IActionResult> GetSuggestionFollowers([FromRoute] int idFollowing, [FromRoute] int idUser, bool isProfile)
        {
            var result = await _userService.GetSuggestionForYouProfile(idFollowing, idUser, isProfile);
            if (result.IsSucess) 
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet("v1/followingfromuser/{idUser}")]
        public async Task<IActionResult> GetUsersFollowignByIdAsync([FromRoute] int idUser)
        {
            var result = await _userService.GetUsersFollowignByIdAsync(idUser);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/userDesconnect/{email}")]
        public async Task<IActionResult> UserDisconnected([FromRoute] string email)
        {
            var result = await _userService.UpdateLastDisconnectedTimeUser(email);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/userEmailDisconnected/{email}")]
        public async Task<IActionResult> UserDisconnectedEmail([FromRoute] string email)
        {
            var result = await _userService.GetByEmailDisconnected(email);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/user")]
        public async Task<IActionResult> PostAsync(
            [FromBody] UserDTO userDTO)
        {
            try
            {
                var result = await _userService.CreateAsync(userDTO);
                if (result.IsSucess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (DomainValidationException ex)
            {
                var result = ResultService.Fail(ex.Message);
                return BadRequest(result);
            }


        }

        [HttpGet("v1/userLogin/{email}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string email, [FromRoute] string password)
        {
            var result = await _userService.Login(email, password);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/userUpdate")]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            var result = await _userService.UpdateAsync(userDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
