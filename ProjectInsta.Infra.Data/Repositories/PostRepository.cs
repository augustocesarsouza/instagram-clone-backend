using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;
using System.Linq;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _ctx;

        public PostRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<Post>> GetAllPostAsync()
        {
            var posts = await _ctx
                .Posts
                .Include(x => x.User)
                .OrderByDescending(x => x.Id)
                .Select(x =>
                new Post(x.Id, x.Title, x.Url, x.IsImagem,
                new User(x.User.Id, x.User.Name, x.User.ImagePerfil), x.PostLikes.Count(), x.Comments.Count(),
                x.PostLikes.Select(x => new PostLike(x.PostId, x.AuthorId)).ToList()))
                .ToListAsync();

            return posts;
        }

        public async Task<ICollection<Post>> GetVideosForReels()
        {
            var posts = await _ctx
                .Posts
                .Where(x => x.IsImagem == 0)
                .OrderByDescending(x => x.CounterOfLikes)
                .Select(x =>
                new Post(x.Id, x.Title, x.Url, x.ImgFrameVideoUrl,
                new User(x.User.Id, x.User.Name, x.User.ImagePerfil), x.PostLikes.Count(), x.Comments.Count(),
                x.PostLikes.Select(x => new PostLike(x.PostId, x.AuthorId)).ToList()))
                .ToListAsync();

            return posts;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            //var post = await _ctx
            //    .Posts
            //    .Where(x => x.Id == id)
            //    //.Include(x => x.User)
            //    .Include(x => x.PostLikes) // só preciso do AuthorId e PostId
            //    .Include(x => x.Comments) // UserId e PostId
            //    //.ThenInclude(x => x.User)
            //    .FirstOrDefaultAsync();

            var post = await _ctx
                .Posts
                .Where(x => x.Id == id)
                .Select(x => new Post(x.Id, x.Title, x.Url, x.PublicId, x.AuthorId, x.IsImagem))
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<Post> GetCheckUserPost(int authorId)
        {
            return await _ctx
                .Posts
                .Where(x => x.AuthorId == authorId)
                .Select(x => new Post(x.Id, x.Title, x.Url))
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Post>> GetThreeLastPostAsync(int userId)
        {
            var threePosts = await _ctx
                .Posts
                .Where(x => x.AuthorId == userId)
                .OrderByDescending(x => x.Id)
                .Take(3)
                .Select(x => new Post(x.Id, x.Title, x.Url))
                .ToListAsync();

            return threePosts;
        }

        public async Task<ICollection<Post>> GetPostByAythorIdAsync(int authorId)
        {
            //return await _ctx
            //    .Posts
            //    .Where(x => x.AuthorId == authorId)
            //    .OrderByDescending (x => x.Id)
            //    .Select(x => new Post(x.Id, x.Url, x.IsImagem,
            //    new User(x.User.Id, x.User.Name, x.User.ImagePerfil)))
            //    .ToListAsync();

            var postOnlyVideoFrame = await _ctx
                .Posts
                .Where(x => x.AuthorId == authorId)
                .OrderByDescending(x => x.Id)
                .Select(x => new Post(x.Id, x.Url, x.ImgFrameVideoUrl != null ? x.ImgFrameVideoUrl : null, x.IsImagem))
                .ToListAsync();

            return postOnlyVideoFrame;
        }

        public async Task<Post?> GetVideoToReelInfo(int reelId)
        {
            var reelInfo = await _ctx
                .Posts
                .Include(x => x.Comments)
                .Where(r => r.Id == reelId)
                .Select(x =>
                new Post(x.Id, x.Url,
                new User(x.User.Id, x.User.Name, x.User.ImagePerfil), x.PostLikes.Count(), x.Comments.Count(),
                x.PostLikes.Select(x => new PostLike(x.PostId, x.AuthorId)).ToList())).FirstOrDefaultAsync();

            return reelInfo;
        }

        public async Task<Post> GetPostCreate(int postId, bool isProfile)
        {
            var posts = new Post();
            if (isProfile)
            {
                posts = await _ctx.Posts
                .Include(x => x.User)
                .Where(x => x.Id == postId && x.IsImagem == 0)
                .Select(x => new Post(x.Id, x.Url, x.ImgFrameVideoUrl != null ? x.ImgFrameVideoUrl : null, x.IsImagem))
                .FirstOrDefaultAsync();
            }
            else
            {
                posts = await _ctx.Posts
               .Include(x => x.User)
               .Where(x => x.Id == postId)
               .Select(x =>
               new Post(x.Id, x.Title, x.Url, x.IsImagem,
               new User(x.User.Id, x.User.Name, x.User.ImagePerfil), x.PostLikes.Count(), x.Comments.Count(),
               x.PostLikes.Select(x => new PostLike(x.PostId, x.AuthorId)).ToList()))
               .FirstOrDefaultAsync();

            }

            return posts;
        }

        public async Task<Post> CreatePostAsync(Post post, bool isProfile)
        {
            await _ctx.Posts.AddAsync(post);
            await _ctx.SaveChangesAsync();

            var postCreate = await GetPostCreate(post.Id, isProfile);

            return postCreate;
        }

        public async Task<Post> UpdatePostAsync(Post post)
        {
            _ctx.Posts.Update(post);
            await _ctx.SaveChangesAsync();
            return post;
        }

        public async Task<Post> DeletePostAsync(Post post)
        {
            _ctx.Posts.Remove(post);
            await _ctx.SaveChangesAsync();
            return post;
        }
    }
}
