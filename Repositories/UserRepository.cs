using CarRentalSystem.Data;
using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public class UserRepository
    {
        private readonly DbContextcs _dbContext;

        public UserRepository(DbContextcs dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddUser(UserModel newUser)
        {
            if (newUser == null)
            {
                return "User object is null.";
            }

            if (string.IsNullOrWhiteSpace(newUser.Name))
            {
                return "User name is required.";
            }

            if (string.IsNullOrWhiteSpace(newUser.Email))
            {
                return "User email is required.";
            }

            if (string.IsNullOrWhiteSpace(newUser.Password) || newUser.Password.Length < 8)
            {
                return "Password must be at least 8 characters long.";
            }

            if (_dbContext.Users.Any(u => u.Email == newUser.Email))
            {
                return "A user with the given email already exists.";
            }

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            return "User added successfully.";
        }

        public (UserModel user, string message) GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return (null, "Email is required.");
            }

            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return (null, $"No user found with email: {email}");
            }

            return (user, "User retrieved successfully.");
        }

        public (UserModel user, string message) GetUserById(int userId)
        {
            if (userId <= 0)
            {
                return (null, "Invalid user ID.");
            }

            var user = _dbContext.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return (null, $"No user found with ID: {userId}");
            }

            return (user, "User retrieved successfully.");
        }
    }
}
