using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class CommentLikeMap : IEntityTypeConfiguration<CommentLike>
    {
        public void Configure(EntityTypeBuilder<CommentLike> builder)
        {
            builder.ToTable("CommentLike");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.HasOne(x => x.Comment)
                .WithMany(x => x.CommentLikes);

            builder.HasOne(x => x.User)
                .WithMany(x => x.CommentLikes);

        }
    }
}
