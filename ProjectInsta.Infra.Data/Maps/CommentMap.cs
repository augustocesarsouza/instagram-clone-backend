using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Text)
                .IsRequired()
                .HasColumnName("Text");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            builder.Property(x => x.PostId)
                .IsRequired()
                .HasColumnName("PostId");

            builder.Ignore(x => x.SubCommentsCounts);
            builder.Ignore(x => x.LikeCommentsCounts);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comments);

            builder.HasMany(x => x.SubComments)
                .WithOne(x => x.Comment)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(x => x.CommentLikes)
                .WithOne(x => x.Comment)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.LikeComments)
                .WithOne(x => x.Comment)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
