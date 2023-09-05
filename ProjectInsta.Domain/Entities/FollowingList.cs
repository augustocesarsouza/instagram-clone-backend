namespace ProjectInsta.Domain.Entities
{
    public class FollowingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string ImagePerfil { get; set; }
        public DateTime? LastDisconnectedTime { get; set; }

        public FollowingList()
        {
        }

        public FollowingList(int id, string name, string? email, string imagePerfil, DateTime? lastDisconnectedTime)
        {
            Id = id;
            Name = name;
            Email = email;
            ImagePerfil = imagePerfil;
            LastDisconnectedTime = lastDisconnectedTime;
        }
    }
}
