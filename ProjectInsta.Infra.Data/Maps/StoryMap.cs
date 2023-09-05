using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class StoryMap : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.ToTable("Story");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Url)
                .IsRequired(false)
                .HasColumnName("Url");

            builder.Property(x => x.PublicId)
                .IsRequired(false)
                .HasColumnName("PublicId");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.Property(x => x.AuthorId)
                .IsRequired()
                .HasColumnName("AuthorId");

            builder.Property(x => x.IsImagem)
                .IsRequired(false)
                .HasColumnName("IsImagem");

            builder.HasOne(x => x.Author);

            builder.HasOne(x => x.PropertyText)
                .WithOne(x => x.Story);

            //builder.HasMany(x => x.StoryVisualizeds)
            //    .WithOne(x => x.Story)
            //    .HasForeignKey("StoryId");

        }
    }
}
