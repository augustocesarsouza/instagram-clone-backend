namespace ProjectInsta.Application.DTOs
{
    public class PostLikeDTO
    {
        public int? Id { get; set; }
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public int? CounterOfLikesPost { get; set; }
    }
}
