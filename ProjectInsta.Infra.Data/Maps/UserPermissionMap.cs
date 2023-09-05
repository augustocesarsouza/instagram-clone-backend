using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Infra.Data.Maps
{
    public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermission");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn()
                .HasColumnName("Id");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            builder.Property(x => x.PermissionId)
                .IsRequired()
                .HasColumnName("PermissionId");

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.UserPermissions);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserPermissions);
        }
    }
}
