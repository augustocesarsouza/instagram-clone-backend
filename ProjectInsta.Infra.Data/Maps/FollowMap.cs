using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class FollowMap : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.ToTable("Follow");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.FollowerId)
                .IsRequired()
                .HasColumnName("FollowerId");

            builder.Property(x => x.FollowingId)
                .IsRequired()
                .HasColumnName("FollowingId");

            builder.HasOne(x => x.Follower)
                .WithMany(x => x.Following)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Following)
                .WithMany(x => x.Followers)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
