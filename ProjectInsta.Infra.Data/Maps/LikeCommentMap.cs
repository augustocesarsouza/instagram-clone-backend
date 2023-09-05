using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class LikeCommentMap : IEntityTypeConfiguration<LikeComment>
    {
        public void Configure(EntityTypeBuilder<LikeComment> builder)
        {
            builder.ToTable("LikeComment");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.CommentId)
                .IsRequired()
                .HasColumnName("CommentId");

            builder.Property(x => x.AuthorId)
                .IsRequired()
                .HasColumnName("AuthorId");

            builder.HasOne(x => x.Comment)
                .WithMany(x => x.LikeComments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Author)
                .WithMany(x => x.LikeComments)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
