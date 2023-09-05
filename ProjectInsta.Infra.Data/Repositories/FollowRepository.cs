using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Follow> GetByIdAllFollowing(int userId)
        {
            var follow = await _context
                .Follows
                .Where(x => x.FollowerId == userId)
                .Include(x => x.Following)
                .ThenInclude(x => x.Following)
                .Select(x =>
                new Follow(x.Id, x.FollowerId, x.FollowingId,
                new User(x.Following.Id, x.Following.Name, x.Following.ImagePerfil)))
                .FirstOrDefaultAsync();
                

             //.Select(x => new Follow
             // {
             //     Id = x.Id,
             //     FollowerId = x.FollowingId,
             //     FollowingId = x.FollowingId,
             //     Following = new User
             //     {
             //         Id = x.Following.Id,
             //         Name = x.Following.Name,
             //         ImagePerfil = x.Following.ImagePerfil,

             //     }
             // })

            return follow;
        }

        public async Task<ICollection<Follow>> GetAllFollowersFromUser(int userId)
        {
            //Get só para pegar para Min tela que abre para inspecionar o usuario
            var allFollowing = await _context
                .Follows
                .Where(x => x.FollowingId == userId)
                .ToListAsync();

            return allFollowing;
        }

        public async Task<ICollection<Follow>> GetAllFollowingFromUser(int userId)
        {
            //Get só para pegar para Min tela que abre para inspecionar o usuario
            var allFollowing = await _context
                .Follows
                .Where(x => x.FollowerId == userId)
                .ToListAsync();

            return allFollowing;
        }

        public async Task<Follow> GetByFollowerAndFollowingAsync(int? followerId, int? followingId)
        {
            var follow = await _context
                .Follows
                .FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowingId == followingId);

            return follow;
        }

        public async Task<Follow> CreateAsync(Follow follow)
        {
            await _context
                .Follows
                .AddAsync(follow);

            await _context
                .SaveChangesAsync();

            return follow;
        }

        public async Task<Follow> DeleteAsync(Follow follow)
        {
            _context
                .Follows
                .Remove(follow);

            await _context
                .SaveChangesAsync();

            return follow;
        }
    }
}
