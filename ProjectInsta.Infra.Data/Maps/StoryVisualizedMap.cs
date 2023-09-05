using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class StoryVisualizedMap : IEntityTypeConfiguration<StoryVisualized>
    {
        public void Configure(EntityTypeBuilder<StoryVisualized> builder)
        {
            builder.ToTable("StoryVisualized");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.UserViewedId)
                .IsRequired()
                .HasColumnName("UserViewedId");

            builder.Property(x => x.UserCreatedPostId)
                .IsRequired()
                .HasColumnName("UserCreatedPostId");

            builder.Property(x => x.StoryId)
                .IsRequired()
                .HasColumnName("StoryId");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.HasOne(x => x.UserViewed);
            builder.HasOne(x => x.UserCreatedPost);

            //builder.HasOne(x => x.Story)
            //    .WithMany(x => x.StoryVisualizeds)
            //    .HasForeignKey("StoryId");
        }
    }
}
