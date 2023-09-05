using Microsoft.IdentityModel.Tokens;
using ProjectInsta.Domain.Authentication;
using ProjectInsta.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectInsta.Infra.Data.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public TokenOutValue Generator(User user, ICollection<UserPermission> userPermissions)
        {
            var permission = string.Join(",", userPermissions.Select(x => x.Permission?.PermissionName));
            var claims = new List<Claim>
            {
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("userId", user.Id.ToString()),
                new Claim("Permissioes", permission)
            };

            var expires = DateTime.Now.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("N0c4bDQyS0k5VTV6Ujl0bWI1aEtRQ2E3ZTc2ZDI2"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new TokenOutValue
            {
                Acess_token = token,
                Expirations = expires,
            };
        }
    }
}
