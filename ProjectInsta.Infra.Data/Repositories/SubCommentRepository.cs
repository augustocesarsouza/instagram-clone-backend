using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class SubCommentRepository : ISubCommentRepository
    {
        private readonly ApplicationDbContext _ctx;

        public SubCommentRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<SubComment>> GetAllAsync()
        {
            return await _ctx.SubComments.Include(x => x.User).ToListAsync();
        }

        public async Task<SubComment> GetByIdCreateAsync(int id)
        {
             var subs = await _ctx.SubComments
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .Select(x => new SubComment (x.Id, x.Text, x.CommentId, new User(x.User.Id, x.User.Name, x.User.ImagePerfil)))
                .FirstOrDefaultAsync();

            return subs;

            //.Select(x => new SubComment
            // {
            //     Id = x.Id,
            //     Text = x.Text,
            //     CommentId = x.CommentId,
            //     User = new User
            //     {
            //         Id = x.User.Id,
            //         Name = x.User.Name,
            //         ImagePerfil = x.User.ImagePerfil,
            //     }
            // })
        }

        public async Task<ICollection<SubComment>> GetByCommentId(int commentId)
        {
             var subComments = await _ctx
                .SubComments
                .Where(x => x.CommentId == commentId)
                .ToListAsync();

            return subComments;
        }

        public async Task<ICollection<SubComment>> GetByCommentIdAsync(int commentId, int pagina, int registroPorPagina)
        {
            return await _ctx.SubComments
                .Include(x => x.User)
                .Where(x => x.CommentId == commentId)
                .OrderByDescending(x => x.Id)
                .Skip((pagina - 1) * registroPorPagina)
                .Take(registroPorPagina)
                .Select(x => new SubComment(x.Id, x.Text, x.CommentId, new User(x.User.Id, x.User.Name, x.User.ImagePerfil)))
                .ToListAsync();
            //.Select(x => new SubComment
            // {
            //     Id = x.Id,
            //     Text = x.Text,
            //     CommentId = x.CommentId,
            //     User = new User
            //     {
            //         Id = x.User.Id,
            //         Name = x.User.Name,
            //         ImagePerfil = x.User.ImagePerfil,
            //     }
            // })
        }

        public async Task<SubComment> CreateAsync(SubComment subComment)
        {
            await _ctx.SubComments.AddAsync(subComment);
            await _ctx.SaveChangesAsync();

            return subComment;
        }

        public async Task<SubComment> DeleteAsync(SubComment subComment)
        {
            _ctx.SubComments.Remove(subComment);
            await _ctx.SaveChangesAsync();

            return subComment;
        }
    }
}
