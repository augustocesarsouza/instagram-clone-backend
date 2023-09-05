namespace ProjectInsta.Application.DTOs
{
    public class FollowDTO
    {
        public int? Id { get; set; }
        public int? FollowerId { get; set; }
        public int? FollowingId { get; set; }
        public ICollection<UserDTO> Followings { get; set; } = new List<UserDTO>();
        public ICollection<UserDTO> Followers { get; set; } = new List<UserDTO>();

    }
}
