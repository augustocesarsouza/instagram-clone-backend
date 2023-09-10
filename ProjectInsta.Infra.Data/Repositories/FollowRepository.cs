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
