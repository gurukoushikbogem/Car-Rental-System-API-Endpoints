using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CarRentalSystem.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public AuthMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            var token = ExtractTokenFromHeader(authorizationHeader);

            if (!string.IsNullOrEmpty(token) && ValidateJwtToken(token, out var userId))
            {
                context.Items["User"] = userId;
            }

            await _next(context);
        }

        private string? ExtractTokenFromHeader(string? authorizationHeader)
        {
            if (!string.IsNullOrWhiteSpace(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }

        private bool ValidateJwtToken(string token, out string? userId)
        {
            userId = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                    return userId != null;
                }
            }
            catch (Exception)
            {
                // Log or handle token validation failures if needed
            }
            return false;
        }
    }
}
