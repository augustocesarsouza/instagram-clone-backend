using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class PostLike
    {
        public int Id { get; private set; }

        public int PostId { get; private set; }
        public Post Post { get; private set; }
        public int AuthorId { get; private set; }
        public User User { get; private set; }
        public int CounterOfLikesPost { get; private set; }

        //public PostLike(int id, int postId, int authorId)
        //{
        //    Id = id;
        //    PostId = postId;
        //    AuthorId = authorId;
        //}

        public PostLike(int postId, int authorId, int counterOfLikesPost)
        {
            PostId = postId;
            AuthorId = authorId;
            CounterOfLikesPost = counterOfLikesPost;
        }


        public PostLike(int postId, int authorId)
        {
            Validator(postId, authorId);
        }

        public void Validator(int postId, int authorId)
        {
            DomainValidationException.When(postId <= 0, "postId deve ser maior que zero");
            DomainValidationException.When(authorId <= 0, "authorId deve ser maior que zero");

            PostId = postId;
            AuthorId = authorId;
        }
    }
}
