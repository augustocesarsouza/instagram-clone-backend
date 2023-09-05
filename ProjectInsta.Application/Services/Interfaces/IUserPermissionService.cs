using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IUserPermissionService
    {
        public Task<ResultService<ICollection<UserPermissionDTO>>> GetAllPermissionUser(int idUser);
    }
}
