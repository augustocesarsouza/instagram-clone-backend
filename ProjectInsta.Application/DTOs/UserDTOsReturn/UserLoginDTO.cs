namespace ProjectInsta.Application.DTOs.UserDTOsReturn
{
    public class UserLoginDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string ImagePerfil { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Token { get; set; }
    }
}
