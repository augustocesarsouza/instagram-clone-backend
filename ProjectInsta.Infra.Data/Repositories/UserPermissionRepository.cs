using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public UserPermissionRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<ICollection<UserPermission>> GetAllPermissionUser(int idUser)
        {
            var permission = await
                _context
                .UserPermissions
                .Include(x => x.Permission)
                .Where(x => x.UserId == idUser)
                .Select(x =>  new UserPermission(x.Id, x.UserId, new Permission(x.Permission.VisualName, x.Permission.PermissionName)))
                .ToListAsync();

            //.Select(x => new UserPermission
            // {
            //     Id = x.Id,
            //     UserId = x.UserId,
            //     Permission = new Permission
            //     {
            //         PermissionName = x.Permission.PermissionName,
            //         VisualName = x.Permission.VisualName,
            //     },

            // })

            return permission;
        }
    }
}
