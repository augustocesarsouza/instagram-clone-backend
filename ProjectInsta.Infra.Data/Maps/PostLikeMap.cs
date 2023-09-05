using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class PostLikeMap : IEntityTypeConfiguration<PostLike>
    {
        public void Configure(EntityTypeBuilder<PostLike> builder)
        {
            builder.ToTable("PostLike");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Ignore(x => x.CounterOfLikesPost);

            builder.HasOne(x => x.User)
                .WithMany(x => x.PostLikes)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.PostLikes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
