﻿using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        //[HttpGet("v1/followallfollowersfromuser/{userId}")]
        [HttpGet("v1/follow/user/follower/all/{userId}")]
        public async Task<ActionResult> GetAllFollowersFromUser([FromRoute] int userId)
        {
            var resut = await _followService.GetAllFollowersFromUser(userId);
            if (resut.IsSucess)
                return Ok(resut);

            return BadRequest(resut);
        }

        //[HttpGet("v1/followallfollowingfromuser/{userId}")]
        [HttpGet("v1/follow/user/following/all/{userId}")]
        public async Task<ActionResult> GetAllFollowingFromUser([FromRoute] int userId)
        {
            var resut = await _followService.GetAllFollowingFromUser(userId);
            if (resut.IsSucess)
                return Ok(resut);

            return BadRequest(resut);
        }

        [HttpPost("v1/follow")]
        public async Task<ActionResult> CreateAsync([FromBody] FollowDTO followDTO)
        {
            var result = await _followService.CreateAsync(followDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/follow")]
        public async Task<ActionResult> DeleteAsync([FromBody] FollowDTO followDTO)
        {
            var result = await _followService.DeleteAsync(followDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
