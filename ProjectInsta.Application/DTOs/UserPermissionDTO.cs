using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class UserPermissionDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int PermissionId { get; set; }
        public PermissionDTO Permission { get; set; }
    }
}
