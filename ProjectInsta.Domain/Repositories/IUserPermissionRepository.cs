using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IUserPermissionRepository
    {
        public Task<ICollection<UserPermission>> GetAllPermissionUser(int idUser);
    }
}
