

namespace WebAPI.Infrastructure.Settings
{
    public class JwtSettings
    {
        public const string Section = "JwtSettings";


        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Secret { get; set; } = default!;
        public int TokenExpiryInMinutes { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }
    }
}
