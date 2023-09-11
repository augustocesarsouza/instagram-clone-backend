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

        [HttpGet("v1/user/data/{idUser}")]
        public async Task<IActionResult> GetUserDataOnly([FromRoute] int idUser)
        {
            var result = await _userService.GetUserDataOnly(idUser);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/user/followers/{idUser}")]
        public async Task<IActionResult> GetFollowersUser([FromRoute] int idUser)
        {
            var result = await _userService.GetFollowersUser(idUser);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/user/following/{idUser}")]
        public async Task<IActionResult> GetUsersFollowignByIdAsync([FromRoute] int idUser)
        {
            var result = await _userService.GetUsersFollowignByIdAsync(idUser);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/user/followers/suggestion/{idFollowing}/{idUser}/{isProfile}")]
        public async Task<IActionResult> GetSuggestionFollowers([FromRoute] int idFollowing, [FromRoute] int idUser, bool isProfile)
        {
            var result = await _userService.GetSuggestionForYouProfile(idFollowing, idUser, isProfile);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/user/following-followers/suggestion/story/{idUser}")]
        public async Task<IActionResult> GetSuggestionToShareReels([FromRoute] int idUser)
        {
            var result = await _userService.GetSuggestionToShareReels(idUser);
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

        [HttpGet("v1/user/login/{email}/{password}")]
        public async Task<IActionResult> Login([FromRoute] string email, [FromRoute] string password)
        {
            var result = await _userService.Login(email, password);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/user/update")]
        public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
        {
            var result = await _userService.UpdateAsync(userDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/user/update/imgperfil/{email}")]
        public async Task<IActionResult> UpdateImgUserPerfil([FromRoute] string email, [FromBody] ImagemBase64ProfileUserDTO imagemBase64ProfileUserDTO)
        {
            var result = await _userService.UpdateImgPerfilUser(email, imagemBase64ProfileUserDTO);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
