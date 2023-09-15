using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Content)
                .IsRequired(false)
                .HasColumnName("Content");

            builder.Property(x => x.Timestamp)
                .IsRequired()
                .HasColumnName("Timestamp");

            builder.Property(x => x.SenderId)
               .IsRequired()
               .HasColumnName("SenderId");

            builder.Property(x => x.RecipientId)
               .IsRequired()
               .HasColumnName("RecipientId");

            builder.Property(x => x.ReelId)
                .IsRequired(false)
                .HasColumnName("ReelId");

            builder.Property(x => x.UrlFrameReel)
                .IsRequired(false)
                .HasColumnName("UrlFrameReel");

            builder.Property(x => x.PublicIdFrameReel)
               .IsRequired(false)
               .HasColumnName("PublicIdFrameReel");

            builder.HasOne(x => x.SenderUser)
                .WithMany(x => x.SenderMessage);

            builder.HasOne(x => x.RecipientUser)
                .WithMany(x => x.RecipientMessage);
        }
    }
}
