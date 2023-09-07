using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;
using System.Linq;

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
            //var allFollowers = _ctx
            //    .Follows
            //    .Where(x => x.FollowingId == userId)
            //    .Select(x => x.FollowerId);

            //var user = await _ctx.Users
            //    .Where(x => allFollowers.Contains(x.Id))
            //    .Select(x => new User(x.Id, x.Name, x.Email, x.ImagePerfil, x.LastDisconnectedTime))
            //    .ToListAsync();

            var user = await _ctx.Users
                .Where(u => _ctx.Follows.Any(f => f.FollowingId == userId && f.FollowerId == u.Id))
                .ToListAsync(); 

            return user;
        }

        public async Task<HashSet<User>> GetSuggestionForYouProfile(int idFollowing, int idUser, bool isProfile)
        {
            if (isProfile)
            {
                var userSuggestion = _ctx.Users
                .Where(u =>
                    u.Id != idUser &&
                    !_ctx.Follows.Any(f => f.FollowingId == idUser && f.FollowerId == u.Id) &&
                    _ctx.Follows.Any(f => f.FollowingId == idFollowing && f.FollowerId == u.Id)
                ).Select(u => new User(u.Id, u.Name, u.Email, u.ImagePerfil)).ToHashSet();

                return userSuggestion;
            }
            else
            {
                var userSuggestion = _ctx.Users
                .Where(u =>
                    !_ctx.Follows.Any(f => f.FollowingId == idUser && f.FollowerId == u.Id) &&
                    _ctx.Follows.Any(f => f.FollowingId == idFollowing && f.FollowerId == u.Id)
                ).Select(u => new User(u.Id, u.Name, u.Email, u.ImagePerfil)).ToHashSet();

                return userSuggestion;
            }

            

            //var allFollowers = _ctx
            //    .Follows
            //    .Where(x => x.FollowingId == idFollowing)
            //    .Select(x => x.FollowerId).ToHashSet();

            //var allFollowersIdUser = _ctx
            //   .Follows
            //   .Where(x => x.FollowingId == idUser)
            //   .Select(x => x.FollowerId).ToHashSet();

            //foreach (var item in allFollowersIdUser)
            //{
            //    allFollowers.Remove(item);
            //}


            //var userSuggestion = _ctx.Users
            //    .Where(x => allFollowers.Contains(x.Id))
            //    .Select(x => new User(x.Id, x.Name, x.Email, x.ImagePerfil))
            //    .ToHashSet();

            //var userProfile = userSuggestion.First(x => x.Id == idUser);
            //userSuggestion.Remove(userProfile);



            
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
