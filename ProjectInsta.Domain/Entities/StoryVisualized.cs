using ProjectInsta.Domain.Validations;

namespace ProjectInsta.Domain.Entities
{
    public class StoryVisualized
    {
        public int Id { get; private set; }

        public int UserViewedId { get; private set; }
        public User UserViewed { get; private set; }

        public int UserCreatedPostId { get; private set; }
        public User UserCreatedPost { get; private set; }

        public int StoryId { get; private set; }
        //public Story Story { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public StoryVisualized()
        { 
        }

        public StoryVisualized(int id)
        {
            Id = id;
        }

        public StoryVisualized(int userViewedId, int storyId)
        {
            UserCreatedPostId = userViewedId;
            StoryId = storyId;
        }

        public StoryVisualized(int userViewedId, int userCreatedPostId, int storyId)
        {
            Validator(userViewedId, userCreatedPostId, storyId);
        }

        public void Validator(int userViewedId, int userCreatedPostId, int storyId)
        {
            DomainValidationException.When(userViewedId <= 0, "userViewedId deve ser maior que zero");
            DomainValidationException.When(userCreatedPostId <= 0, "userCreatedPostId deve ser maior que zero");
            DomainValidationException.When(storyId <= 0, "storyId deve ser maior que zero");
            UserViewedId = userViewedId;
            UserCreatedPostId = userCreatedPostId;
            StoryId = storyId;
            CreatedAt = DateTime.Now;
        }
    }
}
