using Microsoft.AspNetCore.Http;
using System.Linq;
using YourBlog.EfStuff.DbModel;
using YourBlog.EfStuff.Repositories;

namespace YourBlog.Services
{
    public class UserService
    {
        private UserRepository _userRepository;

        private IHttpContextAccessor _httpContextAccessor;

        public UserService(UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrentUser()
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (claim == null)
            {
                return null;
            }
            var idStr = claim.Value;
            var id = int.Parse(idStr);

            var user = _userRepository.Get(id);
            return user;
        }
    }
}
