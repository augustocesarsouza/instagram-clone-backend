namespace ProjectInsta.Application.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string VisualName { get; set; }
        public string PermissionName { get; set; }

        public List<UserPermissionDTO> UserPermissions { get; set; } = new();
    }
}
