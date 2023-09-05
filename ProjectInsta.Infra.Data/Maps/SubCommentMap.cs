using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class SubCommentMap : IEntityTypeConfiguration<SubComment>
    {
        public void Configure(EntityTypeBuilder<SubComment> builder)
        {
            builder.ToTable("SubComment");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.Text)
                .IsRequired()
                .HasColumnName("Text");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnName("CreatedAt");

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt");

            builder.HasOne(x => x.User)
                .WithMany(x => x.SubComments);

            builder.HasOne(x => x.Comment)
                .WithMany(x => x.SubComments);
        }
    }
}
