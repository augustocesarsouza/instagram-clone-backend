using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email");

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash");

            builder.Property(x => x.ImagePerfil)
                .IsRequired()
                .HasColumnName("ImagePerfil");

            builder.Property(x => x.PublicId)
                .IsRequired()
                .HasColumnName("PublicId");

            builder.Property(x => x.BirthDate)
                .IsRequired(false)
                .HasColumnName("BirthDate");

            builder.Property(x => x.LastDisconnectedTime)
                .IsRequired(false)
                .HasColumnName("LastDisconnectedTime");

            builder.Ignore(x => x.Password);
            builder.Ignore(x => x.Token);

            builder.Ignore(x => x.Posts);
            builder.Ignore(x => x.Comments);
            builder.Ignore(x => x.SubComments);
            builder.Ignore(x => x.CommentLikes);
            builder.Ignore(x => x.PostLikes);
            builder.Ignore(x => x.SenderMessage);
            builder.Ignore(x => x.RecipientMessage);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.UserPermissions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SubComments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CommentLikes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PostLikes)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Followers)
                .WithOne(x => x.Following)
                .HasForeignKey(x => x.FollowingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Following)
                .WithOne(x => x.Follower)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SenderMessage)
                .WithOne(x => x.SenderUser)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.RecipientMessage)
                .WithOne(x => x.RecipientUser)
                .HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.LikeComments)
                .WithOne(x => x.Author)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
