using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string name, string email, string password);
        Task<string> LoginAsync(string email, string password);
    }
}
