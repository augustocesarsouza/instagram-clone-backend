using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CommentRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<Comment>> GetAllCommentsAsync()
        {
            return await _ctx.Comments.Include(x => x.User).ToListAsync();
        }

        public async Task<ICollection<Comment>> GetByPostIdAsync(int postId)
        {
            var comments = await _ctx.Comments
                .Where(x => x.PostId == postId)
                .Include(x => x.User)
                .Include(x => x.LikeComments)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new Comment(x.Id, x.Text, x.CreatedAt, new User(x.User.Id, x.User.Name, x.User.ImagePerfil), x.SubComments.Count(), x.LikeComments.Count(), 
                x.LikeComments.Select(x => new LikeComment(x.CommentId, x.AuthorId)).ToList()))
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> GetByCommentIdAsync(int commentId)
        {
            var comments = await _ctx.Comments
               .Where(x => x.Id == commentId)
               .FirstOrDefaultAsync();

            return comments;
        }

        public async Task<ICollection<Comment>> GetLikeCommentsInfo(int postId)
        {
            var comments = await _ctx.Comments
                .Where(x => x.PostId == postId)
                .Include(x => x.LikeComments)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new Comment(x.Id, x.LikeComments.Select(x => new LikeComment(x.CommentId, x.AuthorId)).ToList(), x.LikeComments.Count()))
                .ToListAsync();

            return comments;
        }

        public async Task<ICollection<Comment>> GetByPostIdAsyncForReels(int postId)
        {
            var comments = await _ctx.Comments
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new Comment(x.Id, x.Text, x.CreatedAt, new User(x.User.Id,  x.User.Name, x.User.ImagePerfil), x.SubComments.Count(), x.LikeComments.Count(),
                x.LikeComments.Select(x => new LikeComment(x.CommentId, x.AuthorId)).ToList()))
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> GetByPostIdOneAsync(int? id)
        {
            return await _ctx.Comments
                .Where(x => x.Id == id)
                .Include(x => x.User)
                .Select(x => new Comment(x.Id, x.Text, x.CreatedAt, x.PostId, new User(x.User.Id, x.User.Name, x.User.ImagePerfil))).FirstOrDefaultAsync();

        }

        public async Task<Comment> GetByUserIdAndPostId(int? userId, int? postId)
        {
            return await _ctx
                .Comments
                .Include(x => x.SubComments)
                .Where(x => x.PostId == postId)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Comment>> GetByPostIdAll(int? postId)
        {
            var commentAllPost = await _ctx
                .Comments
                .Where(x => x.PostId == postId)
                .ToListAsync();

            return commentAllPost;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _ctx.Comments.AddAsync(comment);
            await _ctx.SaveChangesAsync();

            var commentRetorn = await GetByPostIdOneAsync(comment.Id);

            return commentRetorn;
        }

        public async Task<Comment> DeleteAsync(Comment comment)
        {
            _ctx.Comments.Remove(comment);
            await _ctx.SaveChangesAsync();

            return comment;
        }
    }
}
