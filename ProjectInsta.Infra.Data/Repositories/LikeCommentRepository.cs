using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class LikeCommentRepository : ILikeCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<LikeComment>> GetAsync()
        {
            return await _context.LikeComments
               .ToListAsync();
        }

        public async Task<LikeComment> GetAuthorIdCommentId(int authorId, int commentId)
        {
            return await _context.LikeComments
                .FirstOrDefaultAsync(x => x.AuthorId == authorId && x.CommentId == commentId);
        }

        public async Task<ICollection<LikeComment>> GetAllLikeCommentId(int commentId)
        {
            var likeComment = await _context.LikeComments
                .Where(x => x.CommentId == commentId)
                .ToListAsync();

            return likeComment;
        }

        public async Task<LikeComment> GetByCommentIdAndAuthorId(int commentId, int authorId)
        {
            var likeComments = await _context.LikeComments
               .Where(x => x.CommentId == commentId && x.AuthorId == authorId)
               .FirstOrDefaultAsync();

            return likeComments;
        }

        public async Task<ICollection<LikeComment>> GetByCommentId(int? commentId)
        {
            var likeComments = await _context.LikeComments
               .Where(x => x.CommentId == commentId)
               .ToListAsync();

            return likeComments;
        }

        public async Task<LikeComment> CreateAsync(LikeComment likeComment)
        {
            await _context.LikeComments.AddAsync(likeComment);
            await _context.SaveChangesAsync();

            return likeComment;
        }

        public async Task<LikeComment> RemoveAsync(LikeComment likeComment)
        {
            _context.LikeComments.Remove(likeComment);
            await _context.SaveChangesAsync();

            return likeComment;
        }
    }
}
