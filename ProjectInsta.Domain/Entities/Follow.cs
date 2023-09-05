using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class Follow
    {
        public int Id { get; private set; }

        public int? FollowerId { get; private set; }
        public User Follower { get; private set; }

        public int? FollowingId { get; private set; }
        public User Following { get; private set; }

        public Follow()
        {
        }

        public Follow(int id, int? followerId, int? followingId)
        {
            Id = id;
            FollowerId = followerId;
            FollowingId = followingId;
        }

        public Follow(int id, int? followerId, int? followingId, User following) : this(id, followerId, followingId)
        {
            Following = following;
        }

        public Follow(int? followerId, int? followingId)
        {
            Validator(followerId, followingId);
        }

        public void Validator(int? followerId, int? followingId)
        {
            DomainValidationException.When(followerId <= 0, "Seguidor deve ser maior que zero!");
            DomainValidationException.When(followingId <= 0, "Seguindo deve ser maior que zero!");

            FollowerId = followerId;
            FollowingId = followingId;
        }
    }
}
