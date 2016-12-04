using Microsoft.AspNetCore.Http;

namespace url_compress_demo.Services
{
    /// <summary>
    /// Служба, получающая id пользователя из текущего http контекста
    /// </summary>
    public class UserService : IUserService
    {
        private IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
