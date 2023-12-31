﻿using ProjectInsta.Application.DTOs;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IFollowService
    {
        public Task<ResultService<ICollection<FollowDTO>>> GetAllFollowingFromUser(int idUser);
        public Task<ResultService<ICollection<FollowDTO>>> GetAllFollowersFromUser(int userId);
        public Task<ResultService<FollowDTO>> CreateAsync(FollowDTO followDTO);
        public Task<ResultService<FollowDTO>> DeleteAsync(FollowDTO followDTO);
    }
}
