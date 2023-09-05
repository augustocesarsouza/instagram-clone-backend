using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class UserRepository : IUserRepostitory
    {
        private readonly ApplicationDbContext _ctx;

        public UserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User?> GetUserDataOnly(int idUser)
        {
            return await _ctx.Users
                .Where(x => x.Id == idUser)
                .Select(x => new User(x.Id, x.Name, x.Email, x.ImagePerfil))
                .FirstOrDefaultAsync();

            //.Select(x => new User
            // {
            //     Id = x.Id,
            //     Name = x.Name,
            //     Email = x.Email,
            //     ImagePerfil = x.ImagePerfil
            // })
        }

        public async Task<User?> GetByEmailIsOnlineAsync(string email)
        {
            return await _ctx.Users
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetFollowersUser(int userId)
        {
            var allFollowers = _ctx
                .Follows
                .Where(x => x.FollowingId == userId)
                .Select(x => x.FollowerId);

            var user = await _ctx.Users
                .Where(x => allFollowers.Contains(x.Id))
                .Select(x => new User(x.Id, x.Name, x.Email, x.ImagePerfil, x.LastDisconnectedTime))
                .ToListAsync();

            return user;
        }

        public async Task<List<User?>> GetUsersFollowignByIdAsync(int idUser)
        {
            //var allFollowing = _ctx
            //    .Follows
            //    .Where(x => x.FollowerId == idUser)
            //    .AsQueryable();

            //var user = await _ctx.Users
            //    .Where(x => allFollowing.Any(f => f.FollowingId == x.Id))
            //    .Select(x => new User(x.Id, x.Name, x.ImagePerfil, x.Email, x.LastDisconnectedTime))
            //    .ToListAsync();

            var allFollowing = _ctx
                .Follows
                .Where(x => x.FollowerId == idUser)
                .Select(x => x.FollowingId);

            var user = await _ctx.Users
                .Where(x => allFollowing.Contains(x.Id))
                .Select(x => new User(x.Id, x.Name, x.Email, x.ImagePerfil, x.LastDisconnectedTime))
                .ToListAsync();

            return user;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var userPer = await _ctx.Users
                .Where(x => x.Email == email)
                .Select(x => new User(x.Id, x.Name, x.Email, x.PasswordHash, x.Token))
                .FirstOrDefaultAsync();

            //.Select(x => new User
            // {
            //     Id = x.Id,
            //     Name = x.Name,
            //     Email = x.Email,
            //     Token = x.Token,
            //     PasswordHash = x.PasswordHash,
            // })

            return userPer;
        }

        public async Task<User?> GetByEmailSignalR(string email)
        {
            var userPer = await _ctx.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            return userPer;
        }

        public async Task<User?> GetByEmailDisconnected(string email)
        {
            return await _ctx.Users
                .Include(x => x.UserPermissions)
                .ThenInclude(x => x.Permission)
                .Where(x => x.Email == email)
                .Select(x => new User(x.Id, x.Name, x.Email, x.LastDisconnectedTime))
                .FirstOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _ctx.Users.Update(user);
            await _ctx.SaveChangesAsync();
            return user;
        }
    }
}
