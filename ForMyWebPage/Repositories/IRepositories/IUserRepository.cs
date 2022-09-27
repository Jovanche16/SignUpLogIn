using ForMyWebPage.Models;

namespace ForMyWebPage.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmail (string email); 
        Task<User> GetByUsername (string username);
        Task<User> CreateUser (User user);
        Task<User> GetById(int? userId);
    }
}
