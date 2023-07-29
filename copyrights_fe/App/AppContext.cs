using System.IdentityModel.Tokens.Jwt;

namespace copyrights_fe.App
{
    public static class AppContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current => _httpContextAccessor.HttpContext;
        public static HttpContext HttpContext => _httpContextAccessor.HttpContext;
        public static int UserId()
        {
            var stringId = HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            int.TryParse(stringId ?? "0", out int userId);

            return userId;
        }

    }
}