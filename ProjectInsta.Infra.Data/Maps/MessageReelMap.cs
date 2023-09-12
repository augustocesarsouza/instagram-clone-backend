using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class MessageReelMap : IEntityTypeConfiguration<MessageReel>
    {
        public void Configure(EntityTypeBuilder<MessageReel> builder)
        {
            builder.ToTable("MessageReel");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(x => x.MessageId)
                .IsRequired()
                .HasColumnName("MessageId");

            builder.Property(x => x.ReelId)
                .IsRequired()
                .HasColumnName("ReelId");

            builder.Property(x => x.Timestamp)
                .IsRequired()
                .HasColumnName("Timestamp");

            builder.HasOne(x => x.Message);

            builder.HasOne(x => x.Reel);
        }
    }
}
