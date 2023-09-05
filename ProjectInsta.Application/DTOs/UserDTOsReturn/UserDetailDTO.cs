namespace ProjectInsta.Application.DTOs.UserDTOsReturn
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public string ImagePerfil { get; set; }
        public string? PublicId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthDateString { get; set; }
        public string? Password { get; set; }
        public string Token { get; set; }
    }
}
