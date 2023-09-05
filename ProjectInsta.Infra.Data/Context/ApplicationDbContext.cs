using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<LikeComment> LikeComments{ get; set; }
        public DbSet<FriendRequest> FriendRequests{ get; set; }
        public DbSet<Story> Stories{ get; set; }
        public DbSet<StoryVisualized> StoryVisualizeds{ get; set; }
        public DbSet<PropertyText> PropertyTexts { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
