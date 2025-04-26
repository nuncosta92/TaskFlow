using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new(); // List in memory (for now, we'll switch to DB later)

        public async Task<User> RegisterAsync(string name, string email, string password)
        {
            if (_users.Any(u => u.Email == email))
            {
                throw new Exception("User already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = HashPassword(password)
            };

            _users.Add(user);

            return await Task.FromResult(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = _users.SingleOrDefault(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            var token = $"DUMMY_TOKEN_FOR_USER_{user.Id}"; // In a real app, generate a JWT or similar token here

            return await Task.FromResult(token);
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }


    }
}
