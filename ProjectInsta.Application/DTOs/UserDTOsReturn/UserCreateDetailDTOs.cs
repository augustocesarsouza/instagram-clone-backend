namespace ProjectInsta.Application.DTOs.UserDTOsReturn
{
    public class UserCreateDetailDTOs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImagePerfil { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Password { get; set; }
    }
}
