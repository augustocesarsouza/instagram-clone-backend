using ProjectInsta.Domain.Entities;
using ProjectInsta.Infra.Data.Authentication;

namespace ProjectInsta.Domain.Authentication
{
    public interface ITokenGenerator
    {
        TokenOutValue Generator(User user, ICollection<UserPermission> userPermissions);
    }
}
