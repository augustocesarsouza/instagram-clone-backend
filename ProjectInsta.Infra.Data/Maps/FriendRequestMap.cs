using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class FriendRequestMap : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.ToTable("FriendRequest");
            builder.HasKey(x => x.Id); 
            
            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");  

            builder.Property(x => x.SenderId)
                .IsRequired()
                .HasColumnName("SenderId");

            builder.Property(x => x.RecipientId)
                .IsRequired()
                .HasColumnName("RecipientId");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.HasOne(x => x.Sender);

            builder.HasOne(x => x.Recipient);
        }
    }
}
