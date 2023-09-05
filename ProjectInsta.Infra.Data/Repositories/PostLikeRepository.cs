using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly ApplicationDbContext _context;

        public PostLikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<PostLike>> GetAllAsync()
        {
            return await _context.PostLikes.ToListAsync();
        }

        public async Task<PostLike?> GetByUserIdAndPostId(int userId, int postId)
        {
            return await _context
                .PostLikes
                .Where(x => x.AuthorId == userId && x.PostId == postId)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<PostLike?>> GetByPostIdAll(int postId)
        {
            var postLikes = await _context
                .PostLikes
                .Where(x => x.PostId == postId)
                .ToListAsync();

            return postLikes;
        }

        public async Task<ICollection<PostLike>> GetByPostId(int postId)
        {
            //var number = await _context.PostLikes.Where(x => x.PostId == postId).CountAsync();

            var postLikes = await _context
                .PostLikes
                .Where(x => x.PostId == postId)
                .Select(x => new PostLike(x.PostId, x.AuthorId))
                .ToListAsync();

            return postLikes;
        }

        public async Task<PostLike> CreateAsync(PostLike postLike)
        {
            await _context.PostLikes.AddAsync(postLike);
            await _context.SaveChangesAsync();
            return postLike;
        }

        public async Task<PostLike> DeleteAsync(PostLike postLike)
        {
            _context.PostLikes.Remove(postLike);
            await _context.SaveChangesAsync();

            return postLike;
        }

        
    }
}
