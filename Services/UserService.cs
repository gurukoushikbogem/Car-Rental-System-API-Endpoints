using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRentalSystem.Services
{
    public class UserService
    {
        private readonly DbContextcs _context;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public UserService(IConfiguration configuration, DbContextcs context)
        {
            _context = context;
            _key = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
        }


        public bool RegisterUser(UserModel user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return false; 
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public string GenerateToken(UserModel temp, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, temp.Name),
                new Claim("role", role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(20),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
