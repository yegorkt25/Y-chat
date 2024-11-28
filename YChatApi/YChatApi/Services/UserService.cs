using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YChatApi.Entities;
using YChatApi.Entities.Repositories;

namespace YChatApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _repository;

        public UserService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetCurrentUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = token[7..];

            var decodedToken = handler.ReadJwtToken(jwt);

            var claims = decodedToken.Claims.Select(claim => (claim.Type, claim.Value)).ToList();

            var username = claims.Find(x => x.Type == "sub").Value;

            var user = await _repository.GetUserByUsernameAsync(username);

            return user;
        }
    }
}
