using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class PropertyTextMap : IEntityTypeConfiguration<PropertyText>
    {
        public void Configure(EntityTypeBuilder<PropertyText> builder)
        {
            builder.ToTable("PropertyText");
            //builder.HasKey(x => x.StoryId);

            //builder.Property(x => x.StoryId)
            //    .IsRequired()
            //    .HasColumnName("StoryId");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Top)
                .IsRequired()
                .HasColumnName("Top");

            builder.Property(x => x.Left)
                .IsRequired()
                .HasColumnName("Left");

            builder.Property(x => x.Text)
                .IsRequired()
                .HasColumnName("Text");

            builder.Property(x => x.Width)
                .IsRequired()
                .HasColumnName("Width");

            builder.Property(x => x.Height)
                .IsRequired(false)
                .HasColumnName("Height");

            builder.Property(x => x.Background)
                .IsRequired()
                .HasColumnName("Background");

            builder.Property(x => x.FontFamily)
               .IsRequired()
               .HasColumnName("FontFamily");

            //builder.Property(x => x.StoryIdRefTable)
            //    .IsRequired()
            //    .HasColumnName("StoryIdRefTable");

            builder.Property(x => x.StoryId)
                .IsRequired()
                .HasColumnName("StoryId");

            builder.HasOne(x => x.Story);
        }
    }
}
