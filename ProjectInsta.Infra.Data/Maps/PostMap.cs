using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title");

            builder.Property(x => x.Url)
                .IsRequired(false)
                .HasColumnName("Url");

            builder.Property(x => x.PublicId)
                .IsRequired(false)
                .HasColumnName("PublicId");

            builder.Property(x => x.IsImagem)
                .IsRequired()
                .HasColumnName("IsImagem");

            builder.Property(x => x.CounterOfLikes)
                .IsRequired(false)
                .HasColumnName("CounterOfLikes");

            builder.Ignore(x => x.PostLikesCounts);
            builder.Ignore(x => x.CommentsLikes);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.PostLikes)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
