using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.UserDTOsReturn;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ResultService<UserDTO>> GetUserDataOnly(int idUser);
        public Task<ResultService<UserDTO>> GetByEmailDisconnected(string email);
        public Task<ResultService<List<UserFollowingDTOs>>> GetUsersFollowignByIdAsync(int idUser);
        public Task<ResultService<List<UserFollowersDTOs>>> GetFollowersUser(int userId);
        public Task<ResultService<UserCreateDetailDTOs>> CreateAsync(UserDTO userDTO);
        public Task<ResultService<UserDTO>> UpdateAsync(UserDTO userDTO);
        public Task<ResultService<UserLoginDTO>> Login(string email, string password);
        public Task<ResultService<UserDTO>> UpdateLastDisconnectedTimeUser(string email);

    }
}