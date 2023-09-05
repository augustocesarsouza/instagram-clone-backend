using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.VisualName)
                .IsRequired()
                .HasColumnName("VisualName");

            builder.Property(x => x.PermissionName)
                .IsRequired()
                .HasColumnName("PermissionName");

            builder.HasMany(x => x.UserPermissions)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId);
        }
    }
}
